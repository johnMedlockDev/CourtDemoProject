using CourtDemoProject.CaseManagementSystem.Api.Services;

namespace CourtDemoProject.CaseManagementSystem.Api.Tests.Services;

[TestClass()]
public class CaseDetailServiceTests
{
    private CaseDetailService _service = null!;
    private CaseManagementSystemDbContext _context = null!;

    [TestInitialize]
    public void SetUp()
    {
        var dbName = Guid.NewGuid().ToString();
        var options = new DbContextOptionsBuilder<CaseManagementSystemDbContext>()
            .UseInMemoryDatabase(databaseName: dbName)
            .Options;

        _context = new CaseManagementSystemDbContext(options);
        _service = new CaseDetailService(_context);

        // Seed the database for testing
        _context.CaseDetails.AddRange(
new CaseDetailEntity { CaseDetailId = Guid.NewGuid(), CaseDetailEntryDateTime = DateTime.Now.AddDays(-1), Description = "First Case Detail Description", DocketDetail = "This is the first docket detail", DocumentUri = null },
new CaseDetailEntity { CaseDetailId = Guid.NewGuid(), CaseDetailEntryDateTime = DateTime.Now, Description = "Second Case Detail Description", DocketDetail = "This is the second docket detail", DocumentUri = null }
        );
        _ = _context.SaveChanges();
    }

    [TestCleanup]
    public void Cleanup()
    {
        _context.Dispose();
    }

    [TestMethod]
    public async Task GetCaseDetailsAsync_ShouldReturnAllCaseDetails()
    {
        var result = await _service.GetCaseDetailsAsync();

        _ = result.Should().NotBeNull();
        _ = result.Should().HaveCount(2);
    }

    [TestMethod]
    public async Task GetCaseDetailAsync_ShouldReturnCaseDetail_WhenExists()
    {
        var existingId = _context.CaseDetails.First().CaseDetailId;
        var result = await _service.GetCaseDetailAsync(existingId);

        _ = result.Should().NotBeNull();
        _ = (result?.CaseDetailId.Should().Be(existingId));
    }

    [TestMethod]
    public async Task AddCaseDetailAsync_ShouldAddNewCaseDetail()
    {
        var newCaseDetail = new CaseDetailDto(
            Guid.NewGuid(),
            DateTime.Now,
            "New Case Detail Description",
            "New Docket Detail",
            new Uri("http://new-document-uri.com")
        );

        var addedCaseDetail = await _service.AddCaseDetailAsync(newCaseDetail);

        _ = addedCaseDetail.Should().NotBeNull();
        _ = addedCaseDetail.CaseDetailId.Should().Be(newCaseDetail.CaseDetailId);
        var caseDetailInDb = await _context.CaseDetails.FindAsync(newCaseDetail.CaseDetailId);
        _ = caseDetailInDb.Should().NotBeNull();
        _ = (caseDetailInDb?.Description.Should().Be("New Case Detail Description"));
    }

    [TestMethod]
    public async Task UpdateCaseDetailAsync_ShouldUpdateCaseDetail_WhenExists()
    {
        var existingCaseDetail = _context.CaseDetails.First();
        var updatedCaseDetailDto = new CaseDetailDto(
            existingCaseDetail.CaseDetailId,
            existingCaseDetail.CaseDetailEntryDateTime.AddDays(1), // Changing date
            "Updated Description",
            existingCaseDetail.DocketDetail,
            existingCaseDetail.DocumentUri
        );

        var result = await _service.UpdateCaseDetailAsync(updatedCaseDetailDto);

        _ = result.Should().BeTrue();
        var updatedCaseDetailInDb = await _context.CaseDetails.FindAsync(existingCaseDetail.CaseDetailId);
        _ = updatedCaseDetailInDb.Should().NotBeNull();
        _ = (updatedCaseDetailInDb?.Description.Should().Be("Updated Description"));
    }

    [TestMethod]
    public async Task DeleteCaseDetailAsync_ShouldRemoveCaseDetail_WhenExists()
    {
        var caseDetailIdToDelete = _context.CaseDetails.First().CaseDetailId;
        await _service.DeleteCaseDetailAsync(caseDetailIdToDelete);

        var deletedCaseDetail = await _context.CaseDetails.FindAsync(caseDetailIdToDelete);
        _ = deletedCaseDetail.Should().BeNull();
    }

    [TestMethod]
    public async Task DeleteCaseDetailAsync_ShouldThrowException_WhenNotExists()
    {
        var nonExistentId = Guid.NewGuid();

        Func<Task> act = async () => await _service.DeleteCaseDetailAsync(nonExistentId);
        _ = await act.Should().ThrowAsync<InvalidOperationException>();
    }
}