using CourtDemoProject.CaseManagementSystem.Api.Services;

namespace CourtDemoProject.CaseManagementSystem.Api.Tests.Services;

[TestClass()]
public class ChargeServiceTests
{
    private ChargeService _service = null!;
    private CaseManagementSystemDbContext _context = null!;

    [TestInitialize]
    public void SetUp()
    {
        var dbName = Guid.NewGuid().ToString();
        var options = new DbContextOptionsBuilder<CaseManagementSystemDbContext>()
            .UseInMemoryDatabase(databaseName: dbName)
            .Options;

        _context = new CaseManagementSystemDbContext(options);
        _service = new ChargeService(_context);

        // Seed the database for testing
        _context.Charges.AddRange(
        new ChargeEntity { ChargeId = Guid.NewGuid(), ChargeCode = "C001", ChargeName = "Charge1", FineAmount = 500, ChargeType = ChargeTypeEnum.Civil, JudgementType = JudgementTypeEnum.Fine, SentenceLengthIndays = 0 },
        new ChargeEntity { ChargeId = Guid.NewGuid(), ChargeCode = "C002", ChargeName = "Charge2", FineAmount = 0, ChargeType = ChargeTypeEnum.Felony, JudgementType = JudgementTypeEnum.Time, SentenceLengthIndays = 3650 }
        );
        _ = _context.SaveChanges();
    }

    [TestCleanup]
    public void Cleanup()
    {
        _context.Dispose();
    }

    [TestMethod]
    public async Task GetChargesAsync_ShouldReturnAllCharges()
    {
        var result = await _service.GetChargesAsync();

        _ = result.Should().HaveCount(2);
        _ = result.First().ChargeName.Should().Be("Charge1");
    }

    [TestMethod]
    public async Task AddChargeAsync_ShouldAddCharge()
    {
        var chargeDto = new ChargeDto(
            Guid.NewGuid(),
            "Charge3",
            "C003",
            ChargeTypeEnum.Felony,
            JudgementTypeEnum.Time,
            300,
            30);

        var result = await _service.AddChargeAsync(chargeDto);

        _ = result.ChargeName.Should().Be(chargeDto.ChargeName);
        _ = _context.Charges.Count().Should().Be(3);
    }

    [TestMethod]
    public async Task UpdateChargeAsync_ShouldReturnTrue_WhenUpdateIsSuccessful()
    {
        var chargeId = Guid.NewGuid();
        var chargeEntity = new ChargeEntity { ChargeId = Guid.NewGuid(), ChargeName = "Charge2", ChargeCode = "C003", ChargeType = ChargeTypeEnum.Civil, JudgementType = JudgementTypeEnum.Fine, FineAmount = 500, SentenceLengthIndays = 0 };
        _ = _context.Charges.Add(chargeEntity);
        _ = await _context.SaveChangesAsync();

        var chargeDto = new ChargeDto(chargeEntity.ChargeId, "UpdatedCharge4", "Charge3", ChargeTypeEnum.Civil, JudgementTypeEnum.Fine, 500, 0);

        var result = await _service.UpdateChargeAsync(chargeDto);

        _ = result.Should().BeTrue();
        var updatedEntity = await _context.Charges.FindAsync(chargeId);
        _ = (updatedEntity?.ChargeId.Should().Be(chargeEntity.ChargeId));
        _ = (updatedEntity?.ChargeName.Should().Be("UpdatedCharge4"));
        _ = (updatedEntity?.ChargeType.Should().Be(ChargeTypeEnum.Civil));
        _ = (updatedEntity?.JudgementType.Should().Be(JudgementTypeEnum.Fine));
        _ = (updatedEntity?.FineAmount.Should().Be(500));
        _ = (updatedEntity?.SentenceLengthIndays.Should().Be(0));
    }

    [TestMethod]
    public async Task DeleteChargeAsync_ShouldReturnTrue_WhenDeleteIsSuccessful()
    {
        // First, add a charge to delete
        var chargeEntity = new ChargeEntity { ChargeId = Guid.NewGuid(), ChargeCode = "C003", ChargeName = "Charge5", FineAmount = 500, ChargeType = ChargeTypeEnum.Civil, JudgementType = JudgementTypeEnum.Time, SentenceLengthIndays = 0 };
        _ = _context.Charges.Add(chargeEntity);
        _ = await _context.SaveChangesAsync();

        // Delete the charge
        var result = await _service.DeleteChargeAsync(chargeEntity.ChargeId);

        _ = result.Should().BeTrue();
        _ = _context.Charges.Any(c => c.ChargeId == chargeEntity.ChargeId).Should().BeFalse();
    }
}