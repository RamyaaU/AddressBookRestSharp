using AddressBookRestSharp;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;

namespace AddressBookTestt
{
    [TestClass]
    public class UnitTest1
    {
        RestClient client;

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
        /// UC 2 Add multiple contacts to the address book JSON server and return the same
        /// </summary>
        [TestMethod]
        public void OnCallingPostAPIForAContactListWithMultipleContacts_ReturnContactObject()
        {
            // Arrange
            List<Contact> contactList = new List<Contact>();
            contactList.Add(new Contact { FirstName = "Ramya", LastName = "U", PhoneNo = "9577456345", Address = "Feroz Shah Kotla", City = "New Delhi", State = "New Delhi", Zip = "547677", Email = "vs@gmail.com" });
            contactList.Add(new Contact { FirstName = "Ankitha", LastName = "H", PhoneNo = "9756723456", Address = "Chinnaswamy", City = "Bangalore", State = "Karnataka", Zip = "435627", Email = "yc@gmail.com" });
            contactList.Add(new Contact { FirstName = "Yashu", LastName = "V", PhoneNo = "9954564345", Address = "Mohali", City = "Mohali", State = "Punjab", Zip = "113425", Email = "klr@gmail.com" });

            //Iterate the loop for each contact
            foreach (var v in contactList)
            {
                //Initialize the request for POST to add new contact
                RestRequest request = new RestRequest("/contacts/list", Method.POST);
                JsonObject jsonObj = new JsonObject();
                jsonObj.Add("firstname", v.FirstName);
                jsonObj.Add("lastname", v.LastName);
                jsonObj.Add("phoneNo", v.PhoneNo);
                jsonObj.Add("address", v.Address);
                jsonObj.Add("city", v.City);
                jsonObj.Add("state", v.State);
                jsonObj.Add("zip", v.Zip);
                jsonObj.Add("email", v.Email);

                //Added parameters to the request object such as the content-type and attaching the jsonObj with the request
                request.AddParameter("application/json", jsonObj, ParameterType.RequestBody);

                //Act
                IRestResponse response = client.Execute(request);

                //Assert
                Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
                Contact contact = JsonConvert.DeserializeObject<Contact>(response.Content);
                Assert.AreEqual(v.FirstName, contact.FirstName);
                Assert.AreEqual(v.LastName, contact.LastName);
                Assert.AreEqual(v.PhoneNo, contact.PhoneNo);
                Console.WriteLine(response.Content);
            }
        }
    }
}
