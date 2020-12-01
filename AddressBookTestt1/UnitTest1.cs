using AddressBookRestSharp;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Net;

namespace AddressBookTestt1
{
    [TestClass]
    public class UnitTest1
    {
        RestClient client;

        /// <summary>
        /// Sets up.
        /// </summary>
        [TestInitialize]
        public void SetUp()
        {
            //Initialize the base URL to execute requests made by the instance
            client = new RestClient("http://localhost:5000");
        }


        /// <summary>
        /// Gets the contact list.
        /// </summary>
        /// <returns></returns>
        private IRestResponse GetContactList()
        {
            //Arrange
            //Initialize the request object with proper method and URL
            RestRequest request = new RestRequest("/contacts/list", Method.GET);
            //Act
            // Execute the request
            IRestResponse response = client.Execute(request);
            return response;
        }

        /// <summary>
        /// Called when [calling put API return contact objects].
        /// </summary>
        [TestMethod]
        public void OnCallingPutAPI_ReturnContactObjects()
        {
            //Arrange
            //Initialize the request for PUT to add new employee
            RestRequest request = new RestRequest("/contacts/7", Method.PUT);
            JsonObject jsonObj = new JsonObject();
            jsonObj.Add("firstname", "Yashu");
            jsonObj.Add("lastname", "V");
            jsonObj.Add("phoneNo", "7858070934");
            jsonObj.Add("address", "FC Real Madrid");
            jsonObj.Add("city", "Madrid");
            jsonObj.Add("state", "Spain");
            jsonObj.Add("zip", "535678");
            jsonObj.Add("email", "yash7@gmail.com");
            //Added parameters to the request object such as the content-type and attaching the jsonObj with the request
            request.AddParameter("application/json", jsonObj, ParameterType.RequestBody);

            //Act
            IRestResponse response = client.Execute(request);

            //Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Contact contact = JsonConvert.DeserializeObject<Contact>(response.Content);
            Assert.AreEqual("Yashu", contact.FirstName);
            Assert.AreEqual("V", contact.LastName);
            Assert.AreEqual("535678", contact.Zip);
            Console.WriteLine(response.Content);
        }
    }
}
