using Amazon.S3.Model;
using AWSLambdaS3.Models;
using Csv;
using System.Globalization;

namespace AWSLambdaS3.Services
{
    public class GetCsvRows : IGetScvRows
    {
        public List<ICsvLine> GetLines(GetObjectResponse file)
        {
            using (var stream = file.ResponseStream)
            {
                var csvLines = Csv.CsvReader.ReadFromStream(stream).ToList();
                return csvLines;
            }
        }

        public List<SimpleRegressionModel> GetSimpleReggressionModel(GetObjectResponse file)
        {
            var result = new List<SimpleRegressionModel>();
            using (var reader = new StreamReader(file.ResponseStream))
            using (var csv = new CsvHelper.CsvReader(reader, CultureInfo.InvariantCulture))
            {
                result = csv.GetRecords<SimpleRegressionModel>().ToList();
            }

            return result;
        }
    }
}
