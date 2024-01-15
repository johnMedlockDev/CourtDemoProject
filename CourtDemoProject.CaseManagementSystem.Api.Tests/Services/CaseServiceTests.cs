using CourtDemoProject.CaseManagementSystem.Api.Services;

namespace CourtDemoProject.CaseManagementSystem.Api.Tests.Services;

[TestClass()]
public class CaseServiceTests
{
    private CaseService _service = null!;
    private CaseManagementSystemDbContext _context = null!;

    [TestInitialize]
    public void SetUp()
    {
        var dbName = Guid.NewGuid().ToString();
        var options = new DbContextOptionsBuilder<CaseManagementSystemDbContext>()
            .UseInMemoryDatabase(databaseName: dbName)
            .Options;

        _context = new CaseManagementSystemDbContext(options);
        _service = new CaseService(_context);

        // Seed the database for testing
        _context.Cases.AddRange(
            new CaseEntity
            {
                CaseId = "Case-1",
                CaseDetails = [new CaseDetailEntity { CaseDetailId = Guid.NewGuid(), CaseDetailEntryDateTime = DateTime.Now.AddDays(-1), Description = "First Case Detail Description", DocketDetail = "This is the first docket detail", DocumentUri = null },
                    new CaseDetailEntity { CaseDetailId = Guid.NewGuid(), CaseDetailEntryDateTime = DateTime.Now, Description = "Second Case Detail Description", DocketDetail = "This is the second docket detail", DocumentUri = null }],
                CaseParticipants = [new CaseParticipantEntity { CaseParticipantEntityId = Guid.NewGuid(), CaseParticipantFirstName = "John", CaseParticipantLastName = "Medlock", CaseParticipantMiddleName = "C", CaseParticipantType = CaseParticipantTypeEnum.Judge },
                    new CaseParticipantEntity { CaseParticipantEntityId = Guid.NewGuid(), CaseParticipantFirstName = "Jim", CaseParticipantLastName = "Tashia", CaseParticipantMiddleName = "Zack", CaseParticipantType = CaseParticipantTypeEnum.Defendant },
                    new CaseParticipantEntity { CaseParticipantEntityId = Guid.NewGuid(), CaseParticipantFirstName = "Sam", CaseParticipantLastName = "Rex", CaseParticipantMiddleName = "Rick", CaseParticipantType = CaseParticipantTypeEnum.Plaintiff }],
                CaseStatus = CaseStatusEnum.InProgress,
                CaseType = CaseTypeEnum.Criminal,
                Charges = [new ChargeEntity { ChargeId = Guid.NewGuid(), ChargeCode = "C001", ChargeName = "Charge1", FineAmount = 500, ChargeType = ChargeTypeEnum.Civil, JudgementType = JudgementTypeEnum.Fine, SentenceLengthIndays = 0 },
                    new ChargeEntity { ChargeId = Guid.NewGuid(), ChargeCode = "C002", ChargeName = "Charge2", FineAmount = 0, ChargeType = ChargeTypeEnum.Felony, JudgementType = JudgementTypeEnum.Time, SentenceLengthIndays = 3650 }],
                CourtDates = [DateTime.Now.AddDays(-1), DateTime.Now],
                CourtName = "Some District Court",
                DateOfOffense = DateOnly.FromDateTime(DateTime.Now.AddDays(-2)),
                Plead = PleadEnum.NotGuilty,
                Verdict = VerdictEnum.InProgress
            }, new CaseEntity
            {
                CaseId = "Case-2",
                CaseDetails = [new CaseDetailEntity { CaseDetailId = Guid.NewGuid(), CaseDetailEntryDateTime = DateTime.Now.AddDays(-1), Description = "Third Case Detail Description", DocketDetail = "This is the third docket detail", DocumentUri = null },
                    new CaseDetailEntity { CaseDetailId = Guid.NewGuid(), CaseDetailEntryDateTime = DateTime.Now, Description = "Forth Case Detail Description", DocketDetail = "This is the forth docket detail", DocumentUri = null }],
                CaseParticipants = [new CaseParticipantEntity { CaseParticipantEntityId = Guid.NewGuid(), CaseParticipantFirstName = "John", CaseParticipantLastName = "Medlock", CaseParticipantMiddleName = "C", CaseParticipantType = CaseParticipantTypeEnum.Judge },
                    new CaseParticipantEntity { CaseParticipantEntityId = Guid.NewGuid(), CaseParticipantFirstName = "Jim", CaseParticipantLastName = "Tashia", CaseParticipantMiddleName = "Zack", CaseParticipantType = CaseParticipantTypeEnum.Defendant },
                    new CaseParticipantEntity { CaseParticipantEntityId = Guid.NewGuid(), CaseParticipantFirstName = "Sam", CaseParticipantLastName = "Rex", CaseParticipantMiddleName = "Rick", CaseParticipantType = CaseParticipantTypeEnum.Plaintiff }],
                CaseStatus = CaseStatusEnum.InProgress,
                CaseType = CaseTypeEnum.Criminal,
                Charges = [new ChargeEntity { ChargeId = Guid.NewGuid(), ChargeCode = "C001", ChargeName = "Charge3", FineAmount = 500, ChargeType = ChargeTypeEnum.Civil, JudgementType = JudgementTypeEnum.Fine, SentenceLengthIndays = 0 },
                    new ChargeEntity { ChargeId = Guid.NewGuid(), ChargeCode = "C002", ChargeName = "Charge4", FineAmount = 0, ChargeType = ChargeTypeEnum.Felony, JudgementType = JudgementTypeEnum.Time, SentenceLengthIndays = 3650 }],
                CourtDates = [DateTime.Now.AddDays(-1), DateTime.Now],
                CourtName = "Some District Court",
                DateOfOffense = DateOnly.FromDateTime(DateTime.Now.AddDays(-2)),
                Plead = PleadEnum.NotGuilty,
                Verdict = VerdictEnum.Guilty
            }

            );
        _ = _context.SaveChanges();
    }

    [TestCleanup]
    public void Cleanup()
    {
        _context.Dispose();
    }

    [TestMethod]
    public async Task GetCasesAsync_ShouldReturnAllCases()
    {
        var result = await _service.GetCasesAsync();

        _ = result.Should().HaveCount(2);
        _ = result.Any(c => c.CaseId == "Case-1").Should().BeTrue();
        _ = result.Any(c => c.CaseId == "Case-2").Should().BeTrue();
    }

    [TestMethod]
    public async Task GetCaseEntityAsync_ShouldReturnCase_WhenCaseExists()
    {
        var result = await _service.GetCaseAsync("Case-1");

        _ = result.Should().NotBeNull();
        _ = (result?.CaseId.Should().Be("Case-1"));
    }
    [TestMethod]
    public async Task AddCaseAsync_ShouldAddCase()
    {
        var newCaseDto = new CaseDto(
             "Case-3",
             "New District Court",
             CaseTypeEnum.Criminal,
             [],
             [],
            DateOnly.FromDateTime(DateTime.Now.AddDays(-3)),
            [],
            VerdictEnum.Guilty,
            PleadEnum.Guilty,
            [DateTime.Now.AddDays(-1), DateTime.Now],  // Initialize with List<DateTime>
            CaseStatusEnum.InProgress
        );

        var addedCaseDto = await _service.AddCaseAsync(newCaseDto);

        _ = addedCaseDto.Should().NotBeNull();
        _ = addedCaseDto.CaseId.Should().Be(newCaseDto.CaseId);
        var cases = await _context.Cases.ToListAsync();
        _ = cases.Should().ContainSingle(c => c.CaseId == newCaseDto.CaseId);
    }

    [TestMethod]
    public async Task UpdateCaseAsync_ShouldReturnTrue_WhenUpdateIsSuccessful()
    {
        var updateCaseDto = new CaseDto(
            "Case-2",
            "Updated District Court",
            CaseTypeEnum.Criminal,
            [],
            [],
            DateOnly.FromDateTime(DateTime.Now.AddDays(-3)),
            [],
            VerdictEnum.Guilty,
            PleadEnum.Guilty,
            [],
            CaseStatusEnum.Closed
        );

        var result = await _service.UpdateCaseAsync(updateCaseDto);

        _ = result.Should().BeTrue();
        var updatedCase = await _context.Cases.FindAsync("Case-2");
        _ = updatedCase.Should().NotBeNull();
        _ = (updatedCase?.CourtName.Should().Be("Updated District Court"));
        _ = (updatedCase?.CaseStatus.Should().Be(CaseStatusEnum.Closed));
    }
    [TestMethod]
    public async Task DeleteCaseAsync_ShouldReturnTrue_WhenDeleteIsSuccessful()
    {
        var result = await _service.DeleteCaseAsync("Case-2");

        _ = result.Should().BeTrue();
        var caseEntity = await _context.Cases.FindAsync("Case-2");
        _ = caseEntity.Should().BeNull();
    }
}