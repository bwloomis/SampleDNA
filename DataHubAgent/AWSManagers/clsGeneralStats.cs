
// my includes
using System.Configuration;
using Amazon.EC2;
using Amazon.EC2.Model;
// using NLog.AWS.Logger;
using NLog;

namespace WindowsFormsApplication2importableCobb.AWSManagers
{
    class clsGeneralStats
    {
        // TODO: switch to AWS NLog - https://github.com/aws/aws-logging-dotnet 
        public static Logger logger { get; set; }

        // ctor
        public clsGeneralStats()
        {
            // Print the number of Amazon EC2 instances.
            IAmazonEC2 ec2 = new AmazonEC2Client();
            DescribeInstancesRequest ec2Request = new DescribeInstancesRequest();

            try
            {
                DescribeInstancesResponse ec2Response = ec2.DescribeInstances(ec2Request);
                int numInstances = 0;
                numInstances = ec2Response.Reservations.Count;
                logger = LogManager.GetLogger("thisapp");
                logger.Trace("You have {0} Amazon EC2 instance(s) running in the {1} region.",
                                               numInstances, ConfigurationManager.AppSettings["AWSRegion"]);
            }
            catch (AmazonEC2Exception ex)
            {
                if (ex.ErrorCode != null && ex.ErrorCode.Equals("AuthFailure"))
                {
                    logger.Error("ERROR: AWS credentials invalid");
                }
                else
                {
                    logger.Error("ERROR: An error occurred with the message '{0}', response status code {1}, error code {2}, error type {3}, and request ID {4}",
                        ex.Message, ex.StatusCode, ex.ErrorCode, ex.ErrorType, ex.RequestId);
                }
            }

            //// add refs above to Amazon.SimpleDB
            //// Print the number of Amazon SimpleDB domains.
            //IAmazonSimpleDB sdb = new AmazonSimpleDBClient();
            //ListDomainsRequest sdbRequest = new ListDomainsRequest();

            //try
            //{
            //    ListDomainsResponse sdbResponse = sdb.ListDomains(sdbRequest);

            //    int numDomains = 0;
            //    numDomains = sdbResponse.DomainNames.Count;
            //    logger.Trace("You have {0} Amazon SimpleDB domain(s) in the {1} region.", numDomains, ConfigurationManager.AppSettings["AWSRegion"]);
            //}
            //catch (AmazonSimpleDBException ex)
            //{
            //    if (ex.ErrorCode != null && ex.ErrorCode.Equals("AuthFailure"))
            //    {
            //        logger.Error("ERROR: AWS credentials invalid");
            //    }
            //    else
            //    {
            //        logger.Error("ERROR: An error occurred with the message '{0}', response status code {1}, error code {2}, error type {3}, and request ID {4}",
            //            ex.Message, ex.StatusCode, ex.ErrorCode, ex.ErrorType, ex.RequestId);
            //    }
            //}
        }
    }
}



       
            