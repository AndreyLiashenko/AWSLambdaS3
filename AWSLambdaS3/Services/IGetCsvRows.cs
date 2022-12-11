using Amazon.S3.Model;
using AWSLambdaS3.Models;
using Csv;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWSLambdaS3.Services
{
    public interface IGetScvRows
    {
        List<ICsvLine> GetLines(GetObjectResponse file);

        List<SimpleRegressionModel> GetSimpleReggressionModel(GetObjectResponse file);
    }
}
