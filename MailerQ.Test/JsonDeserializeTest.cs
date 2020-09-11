﻿using Newtonsoft.Json;
using System.IO;
using System.Text;
using Xunit;

namespace MailerQ.Test
{

    public class JsonDeserializeTest
    {
        private string LoadJsonFromFileExample(string fileName)
        {
            var filePath = Path.Combine("json-examples", fileName);
            var jsonString = File.ReadAllText(filePath, Encoding.UTF8);
            return jsonString;

        }

        [InlineData("result-message.json")]
        [InlineData("result-message-with-added-properties.json")]
        [InlineData("result-message-with-secure-connections.json")]
        [Theory]
        public void Should_parse_result_message_example(string filename)
        {
            // Assert
            var json = LoadJsonFromFileExample(filename);

            // Act
            var result = JsonConvert.DeserializeObject<ResultMessage>(json);
            var success = JsonConvert.DeserializeObject<SuccessMessage>(json);
            var failure = JsonConvert.DeserializeObject<FailureMessage>(json);
            var retry = JsonConvert.DeserializeObject<RetryMessage>(json);

            // Assert
            Assert.IsType<ResultMessage>(result);
            Assert.NotNull(result);
            Assert.IsType<SuccessMessage>(success);
            Assert.NotNull(success);
            Assert.IsType<FailureMessage>(failure);
            Assert.NotNull(failure);
            Assert.IsType<RetryMessage>(retry);
            Assert.NotNull(retry);
        }
    }
}
