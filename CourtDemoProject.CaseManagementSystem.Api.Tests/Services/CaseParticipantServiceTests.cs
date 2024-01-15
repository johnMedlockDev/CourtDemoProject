using CourtDemoProject.CaseManagementSystem.Api.Services;

namespace CourtDemoProject.CaseManagementSystem.Api.Tests.Services;

[TestClass()]
public class CaseParticipantServiceTests
{
    private CaseParticipantService _service = null!;
    private CaseManagementSystemDbContext _context = null!;

    [TestInitialize]
    public void SetUp()
    {
        var dbName = Guid.NewGuid().ToString();
        var options = new DbContextOptionsBuilder<CaseManagementSystemDbContext>()
            .UseInMemoryDatabase(databaseName: dbName)
            .Options;

        _context = new CaseManagementSystemDbContext(options);
        _service = new CaseParticipantService(_context);

        // Seed the database for testing
        _context.CaseParticipants.AddRange(
            new CaseParticipantEntity { CaseParticipantEntityId = Guid.NewGuid(), CaseParticipantFirstName = "John", CaseParticipantLastName = "Medlock", CaseParticipantMiddleName = "C", CaseParticipantType = CaseParticipantTypeEnum.Judge },
            new CaseParticipantEntity { CaseParticipantEntityId = Guid.NewGuid(), CaseParticipantFirstName = "Jim", CaseParticipantLastName = "Tashia", CaseParticipantMiddleName = "Zack", CaseParticipantType = CaseParticipantTypeEnum.Defendant },
            new CaseParticipantEntity { CaseParticipantEntityId = Guid.NewGuid(), CaseParticipantFirstName = "Sam", CaseParticipantLastName = "Rex", CaseParticipantMiddleName = "Rick", CaseParticipantType = CaseParticipantTypeEnum.Plaintiff }
            );
        _ = _context.SaveChanges();
    }

    [TestCleanup]
    public void Cleanup()
    {
        _context.Dispose();
    }

    [TestMethod]
    public async Task GetCaseParticipantsAsync_ShouldReturnAllCaseParticipants()
    {
        var result = await _service.GetCaseParticipantsAsync();

        _ = result.Should().HaveCount(3);
        _ = result.Any(cp => cp.CaseParticipantFirstName == "John").Should().BeTrue();
    }

    [TestMethod]
    public async Task GetCaseParticipantEntityAsync_ShouldReturnCorrectCaseParticipant_WhenIdExists()
    {
        var existingId = _context.CaseParticipants.First().CaseParticipantEntityId;
        var result = await _service.GetCaseParticipantEntityAsync(existingId);

        _ = result.Should().NotBeNull();
        _ = (result?.CaseParticipantEntityId.Should().Be(existingId));
        _ = (result?.CaseParticipantFirstName.Should().Be("John"));
    }

    [TestMethod]
    public async Task AddCaseParticipantEntityAsync_ShouldAddCaseParticipant()
    {
        var newCaseParticipantDto = new CaseParticipantDto(
            Guid.NewGuid(),
             CaseParticipantTypeEnum.Witness,
            "NewFirstName",
            "NewMiddleName",
            "NewLastName"
        );

        var result = await _service.AddCaseParticipantEntityAsync(newCaseParticipantDto);

        _ = result.Should().NotBeNull();
        _ = result.CaseParticipantFirstName.Should().Be("NewFirstName");
        _ = _context.CaseParticipants.Count().Should().Be(4);
    }
    [TestMethod]
    public async Task UpdateCaseParticipantEntityAsync_ShouldUpdateCaseParticipant_WhenIdExists()
    {
        var existingCaseParticipant = _context.CaseParticipants.First();
        var updateDto = new CaseParticipantDto(
            existingCaseParticipant.CaseParticipantEntityId,
            existingCaseParticipant.CaseParticipantType,
            "UpdatedFirstName",
            existingCaseParticipant.CaseParticipantMiddleName,
            existingCaseParticipant.CaseParticipantLastName
        );

        var result = await _service.UpdateCaseParticipantEntityAsync(updateDto);

        _ = result.Should().BeTrue();
        var updatedEntity = await _context.CaseParticipants.FindAsync(existingCaseParticipant.CaseParticipantEntityId);
        _ = (updatedEntity?.CaseParticipantFirstName.Should().Be("UpdatedFirstName"));
    }
    [TestMethod]
    public async Task DeleteCaseParticipantEntityAsync_ShouldDeleteCaseParticipant_WhenIdExists()
    {
        var existingId = _context.CaseParticipants.First().CaseParticipantEntityId;

        var result = await _service.DeleteCaseParticipantEntityAsync(existingId);

        _ = result.Should().BeTrue();
        _ = _context.CaseParticipants.Any(cp => cp.CaseParticipantEntityId == existingId).Should().BeFalse();
    }
}