using System.Collections.Generic;
using System.Linq;
using System.Threading;
using AutoFixture;
using AutoFixture.NUnit3;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using PrologSolution.Data;
using PrologSolution.Data.Entities;
using PrologSolution.Organization.Queries;

namespace PrologTests.UnitTests
{
    public class GetOrganizationQueryTest
    {
        private readonly Mock<IOrganizationService> _mockService;
        private readonly Fixture _fixture = new Fixture();
        public GetOrganizationQueryTest()
        {
            _mockService = new Mock<IOrganizationService>();
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior(1));
        }
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        [TestCase(false,0)]
        [TestCase(true, 4)]
        public void GetOrganizationQuery_Should_Return_Aggregate_Data_Correctly(bool state, int blackListTotal)
        {
            //Arrange
            var request = _fixture.Build<GetOrganizationQuery>().Create();

            var organization = _fixture.Build<Organization>().Create();
            var organizationList = new List<Organization> {organization};

            var user1 = _fixture.Build<User>().Create();
            var user2 = _fixture.Build<User>().Create();
            var userList = new List<User> {user1, user2};

            var phone1 = _fixture.Build<Phone>().With(x=>x.Blacklist,state).Create();
            var phone2 = _fixture.Build<Phone>().With(x=>x.Blacklist,state).Create();
            var phoneList = new List<Phone> {phone1, phone2};

            _mockService.Setup(s => s.GetOrganizationList()).ReturnsAsync(organizationList);
            _mockService.Setup(s => s.GetUsersList(It.IsAny<string>())).ReturnsAsync(userList);
            _mockService.Setup(s => s.GetPhonesList(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(phoneList);

            //Act

            var handler = new GetOrganizationQueryHandler(_mockService.Object);
            var response = handler.Handle(request, CancellationToken.None);

            //Assert
            _mockService.Verify(s => s.GetOrganizationList(),Times.AtMost(2));
            _mockService.Verify(s=>s.GetUsersList(It.IsAny<string>()), Times.AtMost(2));
            _mockService.Verify(s=>s.GetPhonesList(It.IsAny<string>(), It.IsAny<string>()), Times.AtMost(4));
            response.Result.FirstOrDefault()?.BlackListTotal.Should().Be(blackListTotal);
            response.Result.FirstOrDefault()?.TotalCount.Should().Be(4);

        }
    }
}