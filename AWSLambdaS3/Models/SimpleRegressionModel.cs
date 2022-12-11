using CsvHelper.Configuration.Attributes;

namespace AWSLambdaS3.Models
{
    public class SimpleRegressionModel
    {
        [Index(0)]
        public double SAT { get; set; }

        [Index(1)]
        public double GPA { get; set; }
    }
}
