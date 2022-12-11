using Amazon.Lambda.Core;
using Amazon.S3;
using Amazon.S3.Model;
using AWSLambdaS3.Extensions;
using AWSLambdaS3.Models;
using AWSLambdaS3.Services;

namespace AWSLambdaS3;

public class Function
{
    IAmazonS3 S3Client { get; set; }
    private IGetScvRows _csvReader { get; set; }

    public Function()
    {
        S3Client = new AmazonS3Client();
        _csvReader = new GetCsvRows();
    }

    public Function(IAmazonS3 s3Client)
    {
        this.S3Client = s3Client;
        _csvReader = new GetCsvRows();
    }

    [LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]
    public async Task<double> FunctionHandler(InputFunctionModel input, ILambdaContext context)
    {
        if(input is null)
        {
            throw new ArgumentNullException("Input model should be not empty");
        }

        try
        {
            var file = await this.S3Client.GetObjectAsync(input.BucketName, input.FileName);
            var lines = _csvReader.GetSimpleReggressionModel(file);

            double mean = 0;

            if (!string.IsNullOrWhiteSpace(input.ColumnName))
            {
                if (input.ColumnName == "GPA")
                {
                    var gpa = lines.Select(x => x.GPA);
                    var sum = gpa.Sum();
                    mean = sum/gpa.Count();
                }
                else if (input.ColumnName == "SAT")
                {
                    var sat = lines.Select(x => x.SAT);
                    var sum = sat.Sum();
                    mean = sum / sat.Count();
                }
                else
                {
                    throw new Exception("ColumnName is not valid");
                }
            }
            else
            {
                throw new ArgumentException("ColumnName field should not be empty");
            }

            return mean;
        }
        catch (Exception e)
        {
            context.Logger.LogInformation($"Error getting object {input.FileName} from bucket {input.BucketName}. Make sure they exist and your bucket is in the same region as this function.");
            context.Logger.LogInformation(e.Message);
            context.Logger.LogInformation(e.StackTrace);
            throw;
        }
    }
}
