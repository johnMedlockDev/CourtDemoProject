﻿using CourtDemoProject.CaseManagementSystem.Data.Enums;

namespace CourtDemoProject.CaseManagementSystem.Data.Dtos;
public record ChargeDto(Guid ChargeId, string ChargeName, ChargeTypeEnum ChargeEntityType, JudgementTypeEnum JudgementType, double FineAmount, int SentenceLengthIndays);