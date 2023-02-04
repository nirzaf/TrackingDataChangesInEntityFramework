using System.Net;
using Bogus;
using RestSharp;
using TrackingDataChangesInEntityFramework.Controllers;

namespace TestTrackingDataChanges;

public class UnitTest1
{
    [Fact]
    public async Task TestUpdateStudentAPI()
    {
        var baseUrl = "https://localhost:7224/api/";
        var client = new RestClient(baseUrl);
        
        var createStudentRequest = new RestRequest("students", Method.Post);
        var createStudent = new Faker<Student>()
            .RuleFor(s => s.Name, f=> f.Name.FullName())
            .RuleFor(s => s.Email, f => f.Internet.Email())
            .RuleFor(s => s.Phone, f => f.Phone.PhoneNumber())
            .RuleFor(s => s.Address, f => f.Address.StreetAddress())
            .RuleFor(s => s.City, f => f.Address.City())
            .RuleFor(s => s.State, f => f.Address.State())
            .RuleFor(s => s.Zip, f => f.Address.ZipCode());
        
        createStudentRequest.AddJsonBody(createStudent);
        var createStudentResponse = await client.PostAsync<Guid>(createStudentRequest);

        var studentId = createStudentResponse;

        var request = new RestRequest("students", Method.Put);

        var id = "9C81A2A0-73BD-4217-CA6C-08DAEBE941AD";
        //convert id to guid
        var guid = Guid.Parse(id);
        
        var student = new Faker<Student>()
            .RuleFor(s => s.Id, guid)
            .RuleFor(s => s.Name, "")
            .RuleFor(s => s.Email, "")
            .RuleFor(s => s.Phone, "1234567890")
            .RuleFor(s => s.Address, "")
            .RuleFor(s => s.City, "")
            .RuleFor(s => s.State, "")
            .RuleFor(s => s.Zip, "12345");
        
        request.AddJsonBody(student);
        
        var response = await client.PutAsync(request);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}