using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using System;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySearcher.Services
{
    public class AmazonSendService
    {
        private const string bucketName = "jaskiteamlibrary/JSON";

        private IAmazonS3 amazonClient;

        public AmazonSendService(IAmazonS3 amazonClient)
        {
           this.amazonClient = amazonClient;
        }

        public async Task WritingAnObjectAsync(object o, string key)
        {
            try
            {
                var putRequest1 = new PutObjectRequest
                {
                    BucketName = bucketName,
                    Key = "nblablab",
                    ContentBody = o.ToString()

                };

                PutObjectResponse response1 = await amazonClient.PutObjectAsync(putRequest1);
            }
            catch (AmazonS3Exception e)
            {
                Console.WriteLine(
                        "Error encountered ***. Message:'{0}' when writing an object"
                        , e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(
                    "Unknown encountered on server. Message:'{0}' when writing an object"
                    , e.Message);
            }
        }

    }
}
