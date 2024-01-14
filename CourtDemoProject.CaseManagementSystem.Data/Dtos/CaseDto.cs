﻿using CourtDemoProject.CaseManagementSystem.Data.Entities;
using CourtDemoProject.CaseManagementSystem.Data.Enums;

namespace CourtDemoProject.CaseManagementSystem.Data.Dtos;
public record CaseDto(string CaseId, string CourtName, CaseTypeEnum CaseType, ICollection<CaseParticipantDto> CaseParticipants, ICollection<ChargeDto> Charges, DateOnly DateOfOffense, ICollection<CaseDetailDto> CaseDetails, VerdictEnum Verdict, PleadEnum Plead, ICollection<DateTime> CourtDates, CaseStatusEnum CaseStatus)
{
    public CaseEntity ToEntity()
    {

        return new CaseEntity { CaseId = CaseId, CaseType = CaseType, CourtName = CourtName, CaseStatus = CaseStatus, Verdict = Verdict, Plead = Plead, CourtDates = CourtDates, DateOfOffense = DateOfOffense, CaseParticipants = ConvertCaseParticipantDtoToEntities, CaseDetails = ConvertCaseDetailDtoToEntities, Charges = ConvertChargeDtoToEntities };
    }

    private ICollection<CaseParticipantEntity> ConvertCaseParticipantDtoToEntities
    {
        get
        {
            return CaseParticipants.Select(x => new CaseParticipantEntity
            {
                CaseParticipantEntityId = x.CaseParticipantEntityId,
                CaseParticipantFirstName = x.CaseParticipantFirstName,
                CaseParticipantLastName = x.CaseParticipantLastName,
                CaseParticipantMiddleName = x.CaseParticipantMiddleName,
                CaseParticipantType = x.CaseParticipantType
            }).ToList();
        }
    }

    private ICollection<CaseDetailEntity> ConvertCaseDetailDtoToEntities
    {
        get
        {
            return CaseDetails.Select(x => new CaseDetailEntity
            {
                CaseDetailId = x.CaseDetailId,
                CaseDetailEntryDateTime = x.CaseDetailEntryDateTime,
                Description = x.Description,
                DocketDetail = x.DocketDetail,
                DocumentUri = x.DocumentUri

            }).ToList();
        }
    }

    private ICollection<ChargeEntity> ConvertChargeDtoToEntities => Charges.Select(x => new ChargeEntity
    {
        ChargeId = x.ChargeId,
        ChargeType = x.ChargeType,
        ChargeCode = x.ChargeCode,
        ChargeName = x.ChargeName,
        FineAmount = x.FineAmount,
        JudgementType = x.JudgementType,
        SentenceLengthIndays = x.SentenceLengthIndays
    }).ToList();
}