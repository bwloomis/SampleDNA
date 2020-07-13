using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// my includes
using System.Collections.Specialized;
using System.Configuration;
using System.IO;
using Amazon.S3;
using Amazon.S3.Model;
using NLog;

namespace DataHub.DataHubAgent.AWSManagers
{
    class clsS3Manager
    {
        // Change the AWSProfileName to the profile you want to use in the App.config file.
        // See http://aws.amazon.com/credentials  for more details.
        // You must also sign up for an Amazon S3 account for this to work
        // See http://aws.amazon.com/s3/ for details on creating an Amazon S3 account
        // Change the bucketName and keyName fields to values that match your bucketname and keyname
        public string bucketName { get; set; }  // not static
        public string moveToBucketName { get; set; }
        public string keyName { get; set; } // not static

        public static IAmazonS3 client { get; set; }
        private static ILogger _logger { get; set; }

        // ctor
        public clsS3Manager(ILogger logger, string startS3bucket, string endS3bucket)
        {
            _logger = logger;

            bucketName = startS3bucket;
            moveToBucketName = endS3bucket;

            if (string.IsNullOrEmpty(startS3bucket))
            {
                _logger.Trace("INFO: there is no S3 bucket - aborting");
                throw new Exception("No S3 target bucket defined");
            }
            else
            {
                _logger.Trace("INFO: Setting initial S3 bucket to {0}", startS3bucket);
            }

            client = new AmazonS3Client();
            ListBuckets();
        }

        // copy a local file into S3 bucket
        public string UploadFile(string sourceFilePath, string destName)
        {
            string target = "";
            PutObjectRequest request;
            PutObjectResponse response;

            if (string.IsNullOrEmpty(sourceFilePath))
                return target;
            if (!File.Exists(sourceFilePath))
                return target;

            // simple putObject
            try
            {
                if (!BucketExists(bucketName))
                    CreateBucket(bucketName);

                // get file extension
                string ext = Path.GetExtension(sourceFilePath);
                switch (ext) { 
                    case ".json":   // upload a JSON file with Newtonsoft
                        request = new PutObjectRequest()
                        {
                            ContentBody = File.ReadAllText(sourceFilePath),
                            BucketName = bucketName,
                            Key = destName,
                            ContentType = "application/json"
                        };
                        // used to be Key = keyname - which is not used when AWS profile is set; set Key to destination file name instead
                        request.Metadata.Add("title", string.Concat("GeneSeek LIMS Upload from ", sourceFilePath));
                        response = client.PutObject(request);
                        break;
                    case ".txt":    // upload a text file
                        request = new PutObjectRequest()
                        {
                            ContentBody = File.ReadAllText(sourceFilePath),
                            BucketName = bucketName,
                            Key = destName,
                            ContentType = "text/plain"
                        };
                        // used to be Key = keyname - which is not used when AWS profile is set; set Key to destination file name instead
                        request.Metadata.Add("title", string.Concat("GeneSeek LIMS Upload from ", sourceFilePath));
                        response = client.PutObject(request);
                        break;
                    default:        // upload a binary file
                        request = new PutObjectRequest()
                        {
                            BucketName = bucketName,
                            Key = destName,
                            FilePath = sourceFilePath,
                            ContentType = "application/octet-stream"
                        };
                        request.Metadata.Add("title", string.Concat("GeneSeek LIMS Upload from ", sourceFilePath));
                        response = client.PutObject(request);
                        break;
                }
            }
            catch (AmazonS3Exception amazonS3Exception)
            {
                if (amazonS3Exception.ErrorCode != null &&
                    (amazonS3Exception.ErrorCode.Equals("InvalidAccessKeyId") ||
                    amazonS3Exception.ErrorCode.Equals("InvalidSecurity")))
                {
                    _logger.Error("ERROR: AWS credentials invalid");
                }
                else
                {
                    _logger.Error("ERROR: An error occurred with the message '{0}' when writing an object", amazonS3Exception.Message);
                }
            }

            return target;
        }

        // original flow in sample
        // listingBuckets, CreateBucket, WritingAnObject, ReadingObject, DeletingObject, ListingObjects
  
        public bool BucketExists(string b)
        {
            bool resp = false;

            ListBucketsResponse response = client.ListBuckets();
            foreach (S3Bucket bucket in response.Buckets)
                if (bucket.BucketName == b)
                    resp = true;

            return resp;
        }

        public bool CreateBucket(string b)
        {
            if (BucketExists(bucketName))
                return true;

            try
            {
                PutBucketRequest request = new PutBucketRequest();
                request.BucketName = bucketName;
                client.PutBucket(request);
            }
            catch (AmazonS3Exception amazonS3Exception)
            {
                if (amazonS3Exception.ErrorCode != null && (amazonS3Exception.ErrorCode.Equals("InvalidAccessKeyId") || amazonS3Exception.ErrorCode.Equals("InvalidSecurity")))
                {
                    _logger.Error("ERROR: AWS credentials invalid");
                    return false;
                }
                else
                {
                    _logger.Error("ERROR: An error occurred with the message '{0}' and error code {1} when writing an object", amazonS3Exception.Message, amazonS3Exception.ErrorCode);
                    return false;
                }
            }

            return true;
        }

        public void ListBuckets()
        {
            try
            {
                ListBucketsResponse response = client.ListBuckets();
                foreach (S3Bucket bucket in response.Buckets)
                {
                    _logger.Trace("INFO: Bucket listing found {0}", bucket.BucketName);
                }
            }
            catch (AmazonS3Exception amazonS3Exception)
            {
                if (amazonS3Exception.ErrorCode != null &&
                    (amazonS3Exception.ErrorCode.Equals("InvalidAccessKeyId") ||
                    amazonS3Exception.ErrorCode.Equals("InvalidSecurity")))
                {
                    _logger.Error("ERROR: AWS credentials invalid");
                }
                else
                {
                    _logger.Error("ERROR: An error occurred with the message '{0}' and error code {1} when writing an object", amazonS3Exception.Message, amazonS3Exception.ErrorCode);
                }
            }
        }

        public void ListBucketContents()
        {
            return;
        }

        // CONVERTED
        public void WritingAnObject()   // not static
        {
            try
            {
                // TODO: check taregt file does not exist

                // simple object put
                PutObjectRequest request = new PutObjectRequest()
                {
                    ContentBody = "this is a test",
                    BucketName = bucketName,
                    Key = keyName
                };

                PutObjectResponse response = client.PutObject(request);

                // put a more complex object with some metadata and http headers.
                PutObjectRequest titledRequest = new PutObjectRequest()
                {
                    BucketName = bucketName,
                    Key = keyName
                };
                titledRequest.Metadata.Add("title", "the title");

                client.PutObject(titledRequest);
            }
            catch (AmazonS3Exception amazonS3Exception)
            {
                if (amazonS3Exception.ErrorCode != null &&
                    (amazonS3Exception.ErrorCode.Equals("InvalidAccessKeyId") ||
                    amazonS3Exception.ErrorCode.Equals("InvalidSecurity")))
                {
                    _logger.Error("ERROR: AWS credentials invalid");
                }
                else
                {
                    _logger.Error("ERROR: An error occurred with the message '{0}' when writing an object", amazonS3Exception.Message);
                }
            }
        }

        public void ReadingAnObject()
        {
            try
            {
                GetObjectRequest request = new GetObjectRequest()
                {
                    BucketName = bucketName,
                    Key = keyName
                };

                using (GetObjectResponse response = client.GetObject(request))
                {
                    string title = response.Metadata["x-amz-meta-title"];
                    Console.WriteLine("The object's title is {0}", title);
                    string dest = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), keyName);
                    if (!File.Exists(dest))
                    {
                        response.WriteResponseStreamToFile(dest);
                    }
                }
            }
            catch (AmazonS3Exception amazonS3Exception)
            {
                if (amazonS3Exception.ErrorCode != null &&
                    (amazonS3Exception.ErrorCode.Equals("InvalidAccessKeyId") ||
                    amazonS3Exception.ErrorCode.Equals("InvalidSecurity")))
                {
                    Console.WriteLine("Please check the provided AWS Credentials.");
                    Console.WriteLine("If you haven't signed up for Amazon S3, please visit http://aws.amazon.com/s3");
                }
                else
                {
                    Console.WriteLine("An error occurred with the message '{0}' when reading an object", amazonS3Exception.Message);
                }
            }
        }

        public void DeletingAnObject()
        {
            try
            {
                DeleteObjectRequest request = new DeleteObjectRequest()
                {
                    BucketName = bucketName,
                    Key = keyName
                };

                client.DeleteObject(request);
            }
            catch (AmazonS3Exception amazonS3Exception)
            {
                if (amazonS3Exception.ErrorCode != null &&
                    (amazonS3Exception.ErrorCode.Equals("InvalidAccessKeyId") ||
                    amazonS3Exception.ErrorCode.Equals("InvalidSecurity")))
                {
                    Console.WriteLine("Please check the provided AWS Credentials.");
                    Console.WriteLine("If you haven't signed up for Amazon S3, please visit http://aws.amazon.com/s3");
                }
                else
                {
                    Console.WriteLine("An error occurred with the message '{0}' when deleting an object", amazonS3Exception.Message);
                }
            }
        }

        public void ListingObjects()
        {
            try
            {
                ListObjectsRequest request = new ListObjectsRequest();
                request.BucketName = bucketName;
                ListObjectsResponse response = client.ListObjects(request);
                foreach (S3Object entry in response.S3Objects)
                {
                    Console.WriteLine("key = {0} size = {1}", entry.Key, entry.Size);
                }

                // list only things starting with "foo"
                request.Prefix = "foo";
                response = client.ListObjects(request);
                foreach (S3Object entry in response.S3Objects)
                {
                    Console.WriteLine("key = {0} size = {1}", entry.Key, entry.Size);
                }

                // list only things that come after "bar" alphabetically
                request.Prefix = null;
                request.Marker = "bar";
                response = client.ListObjects(request);
                foreach (S3Object entry in response.S3Objects)
                {
                    Console.WriteLine("key = {0} size = {1}", entry.Key, entry.Size);
                }

                // only list 3 things
                request.Prefix = null;
                request.Marker = null;
                request.MaxKeys = 3;
                response = client.ListObjects(request);
                foreach (S3Object entry in response.S3Objects)
                {
                    Console.WriteLine("key = {0} size = {1}", entry.Key, entry.Size);
                }
            }
            catch (AmazonS3Exception amazonS3Exception)
            {
                if (amazonS3Exception.ErrorCode != null && (amazonS3Exception.ErrorCode.Equals("InvalidAccessKeyId") || amazonS3Exception.ErrorCode.Equals("InvalidSecurity")))
                {
                    Console.WriteLine("Please check the provided AWS Credentials.");
                    Console.WriteLine("If you haven't signed up for Amazon S3, please visit http://aws.amazon.com/s3");
                }
                else
                {
                    Console.WriteLine("An error occurred with the message '{0}' when listing objects", amazonS3Exception.Message);
                }
            }
        }
    }
}
