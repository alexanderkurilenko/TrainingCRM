﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Training.Importer.ImportType.Models
{
    [Serializable]
    public class Contact:ImportEntity,ILockable
    {

        public readonly Guid InnerGuid = Guid.NewGuid();

        public string TransactionCurrencyIdName { get; set; }
        public string PreferredSystemUserIdName { get; set; }
        public string kuoni_AgentModifiedByIdName { get; set; }
        public string kuoni_AgencyCreatedByIdYomiName { get; set; }
        public string kuoni_SubstitutionIdName { get; set; }
        public string kuoni_OriginOfSourceIdName { get; set; }
        public string kuoni_ResponsibleIdYomiName { get; set; }
        public string kuoni_AgentModifiedByIdYomiName { get; set; }
        public string kuoni_AgentCreatedByIdName { get; set; }
        public string CreatedOnBehalfByName { get; set; }
        public string CreatedByYomiName { get; set; }
        public string kuoni_AgentCreatedByIdYomiName { get; set; }
        public string kuoni_AgencyCreatedByIdName { get; set; }
        public string MasterContactIdYomiName { get; set; }
        public string kuoni_ParentAgencyIdYomiName { get; set; }
        public string kuoni_ResponsibleIdName { get; set; }
        public string PreferredEquipmentIdName { get; set; }
        public string kuoni_OpenCaseIdName { get; set; }
        public string PreferredSystemUserIdYomiName { get; set; }
        public string MasterContactIdName { get; set; }
        public string OriginatingLeadIdYomiName { get; set; }
        public string kuoni_CurrencyIdName { get; set; }
        public string kuoni_SubstitutionIdYomiName { get; set; }
        public string kuoni_AgencyModifiedByIdYomiName { get; set; }
        public string ModifiedByName { get; set; }
        public string kuoni_SourceSystemIdName { get; set; }
        public string kuoni_AgencyModifiedByIdName { get; set; }
        public string ModifiedOnBehalfByName { get; set; }
        public string CreatedOnBehalfByYomiName { get; set; }
        public string kuoni_NextOrderIdName { get; set; }
        public string ModifiedOnBehalfByYomiName { get; set; }
        public string PreferredServiceIdName { get; set; }
        public string DefaultPriceLevelIdName { get; set; }
        public string OriginatingLeadIdName { get; set; }
        public string kuoni_ParentAgencyIdName { get; set; }
        public string CreatedByName { get; set; }
        public string ModifiedByYomiName { get; set; }
        public Nullable<int> Address1_AddressTypeCode { get; set; }
        public string Address1_City { get; set; }
        public string Address1_Country { get; set; }
        public string Address1_County { get; set; }
        public Nullable<System.Guid> Address1_AddressId { get; set; }
        public string Address1_Fax { get; set; }
        public Nullable<int> Address1_FreightTermsCode { get; set; }
        public Nullable<double> Address1_Latitude { get; set; }
        public string Address1_Line1 { get; set; }
        public string Address1_Line2 { get; set; }
        public string Address1_Line3 { get; set; }
        public Nullable<double> Address1_Longitude { get; set; }
        public string Address1_Name { get; set; }
        public string Address1_PostalCode { get; set; }
        public string Address1_PostOfficeBox { get; set; }
        public string Address1_PrimaryContactName { get; set; }
        public Nullable<int> Address1_ShippingMethodCode { get; set; }
        public string Address1_StateOrProvince { get; set; }
        public string Address1_Telephone1 { get; set; }
        public string Address1_Telephone2 { get; set; }
        public string Address1_Telephone3 { get; set; }
        public string Address1_UPSZone { get; set; }
        public Nullable<int> Address1_UTCOffset { get; set; }
        public Nullable<int> Address2_AddressTypeCode { get; set; }
        public string Address2_City { get; set; }
        public string Address2_Country { get; set; }
        public string Address2_County { get; set; }
        public Nullable<System.Guid> Address2_AddressId { get; set; }
        public string Address2_Fax { get; set; }
        public Nullable<int> Address2_FreightTermsCode { get; set; }
        public Nullable<double> Address2_Latitude { get; set; }
        public string Address2_Line1 { get; set; }
        public string Address2_Line2 { get; set; }
        public string Address2_Line3 { get; set; }
        public Nullable<double> Address2_Longitude { get; set; }
        public string Address2_Name { get; set; }
        public string Address2_PostalCode { get; set; }
        public string Address2_PostOfficeBox { get; set; }
        public string Address2_PrimaryContactName { get; set; }
        public Nullable<int> Address2_ShippingMethodCode { get; set; }
        public string Address2_StateOrProvince { get; set; }
        public string Address2_Telephone1 { get; set; }
        public string Address2_Telephone2 { get; set; }
        public string Address2_Telephone3 { get; set; }
        public string Address2_UPSZone { get; set; }
        public Nullable<int> Address2_UTCOffset { get; set; }
        public System.Guid OwnerId { get; set; }
        public string OwnerIdName { get; set; }
        public string OwnerIdYomiName { get; set; }
        public int OwnerIdDsc { get; set; }
        public Nullable<int> OwnerIdType { get; set; }
        public Nullable<System.Guid> OwningUser { get; set; }
        public Nullable<System.Guid> OwningTeam { get; set; }
        public Nullable<System.Guid> AccountId { get; set; }
        public string AccountIdName { get; set; }
        public string AccountIdYomiName { get; set; }
        public Nullable<System.Guid> ParentContactId { get; set; }
        public string ParentContactIdName { get; set; }
        public string ParentContactIdYomiName { get; set; }
        public System.Guid ContactId { get; set; }
        public Nullable<System.Guid> DefaultPriceLevelId { get; set; }
        public Nullable<int> CustomerSizeCode { get; set; }
        public Nullable<int> CustomerTypeCode { get; set; }
        public Nullable<int> PreferredContactMethodCode { get; set; }
        public Nullable<int> LeadSourceCode { get; set; }
        public Nullable<System.Guid> OriginatingLeadId { get; set; }
        public Nullable<System.Guid> OwningBusinessUnit { get; set; }
        public Nullable<int> PaymentTermsCode { get; set; }
        public Nullable<int> ShippingMethodCode { get; set; }
        public Nullable<bool> ParticipatesInWorkflow { get; set; }
        public Nullable<bool> IsBackofficeCustomer { get; set; }
        public string Salutation { get; set; }
        public string JobTitle { get; set; }
        public string FirstName { get; set; }
        public string Department { get; set; }
        public string NickName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Suffix { get; set; }
        public string YomiFirstName { get; set; }
        public string FullName { get; set; }
        public string YomiMiddleName { get; set; }
        public string YomiLastName { get; set; }
        public Nullable<System.DateTime> Anniversary { get; set; }
        public Nullable<System.DateTime> BirthDate { get; set; }
        public string GovernmentId { get; set; }
        public string YomiFullName { get; set; }
        public string Description { get; set; }
        public string EmployeeId { get; set; }
        public Nullable<int> GenderCode { get; set; }
        public Nullable<decimal> AnnualIncome { get; set; }
        public Nullable<int> HasChildrenCode { get; set; }
        public Nullable<int> EducationCode { get; set; }
        public string WebSiteUrl { get; set; }
        public Nullable<int> FamilyStatusCode { get; set; }
        public string FtpSiteUrl { get; set; }
        public string EMailAddress1 { get; set; }
        public string SpousesName { get; set; }
        public string AssistantName { get; set; }
        public string EMailAddress2 { get; set; }
        public string AssistantPhone { get; set; }
        public string EMailAddress3 { get; set; }
        public Nullable<bool> DoNotPhone { get; set; }
        public string ManagerName { get; set; }
        public string ManagerPhone { get; set; }
        public Nullable<bool> DoNotFax { get; set; }
        public Nullable<bool> DoNotEMail { get; set; }
        public Nullable<bool> DoNotPostalMail { get; set; }
        public Nullable<bool> DoNotBulkEMail { get; set; }
        public Nullable<bool> DoNotBulkPostalMail { get; set; }
        public Nullable<int> AccountRoleCode { get; set; }
        public Nullable<int> TerritoryCode { get; set; }
        public Nullable<bool> IsPrivate { get; set; }
        public Nullable<decimal> CreditLimit { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<bool> CreditOnHold { get; set; }
        public Nullable<System.Guid> CreatedBy { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }
        public Nullable<System.Guid> ModifiedBy { get; set; }
        public Nullable<int> NumberOfChildren { get; set; }
        public string ChildrensNames { get; set; }
        public byte[] VersionNumber { get; set; }
        public string MobilePhone { get; set; }
        public string Pager { get; set; }
        public string Telephone1 { get; set; }
        public string Telephone2 { get; set; }
        public string Telephone3 { get; set; }
        public string Fax { get; set; }
        public Nullable<decimal> Aging30 { get; set; }
        public int StateCode { get; set; }
        public Nullable<decimal> Aging60 { get; set; }
        public Nullable<int> StatusCode { get; set; }
        public Nullable<decimal> Aging90 { get; set; }
        public Nullable<System.Guid> PreferredSystemUserId { get; set; }
        public Nullable<System.Guid> PreferredServiceId { get; set; }
        public Nullable<System.Guid> MasterId { get; set; }
        public Nullable<int> PreferredAppointmentDayCode { get; set; }
        public Nullable<int> PreferredAppointmentTimeCode { get; set; }
        public Nullable<bool> DoNotSendMM { get; set; }
        public Nullable<System.Guid> ParentCustomerId { get; set; }
        public Nullable<bool> Merged { get; set; }
        public string ExternalUserIdentifier { get; set; }
        public Nullable<System.Guid> SubscriptionId { get; set; }
        public Nullable<System.Guid> PreferredEquipmentId { get; set; }
        public Nullable<System.DateTime> LastUsedInCampaign { get; set; }
        public string ParentCustomerIdName { get; set; }
        public Nullable<int> ParentCustomerIdType { get; set; }
        public Nullable<System.Guid> TransactionCurrencyId { get; set; }
        public Nullable<System.DateTime> OverriddenCreatedOn { get; set; }
        public Nullable<decimal> ExchangeRate { get; set; }
        public Nullable<int> ImportSequenceNumber { get; set; }
        public Nullable<int> TimeZoneRuleVersionNumber { get; set; }
        public Nullable<int> UTCConversionTimeZoneCode { get; set; }
        public Nullable<decimal> AnnualIncome_Base { get; set; }
        public Nullable<decimal> CreditLimit_Base { get; set; }
        public Nullable<decimal> Aging60_Base { get; set; }
        public Nullable<decimal> Aging90_Base { get; set; }
        public Nullable<decimal> Aging30_Base { get; set; }
        public string ParentCustomerIdYomiName { get; set; }
        public Nullable<System.Guid> CreatedOnBehalfBy { get; set; }
        public Nullable<System.Guid> ModifiedOnBehalfBy { get; set; }
        public Nullable<bool> IsAutoCreate { get; set; }
        public string kuoni_Address1CareOf { get; set; }
        public string kuoni_Address1FlatNumber { get; set; }
        public string kuoni_Address1FlatSuffix { get; set; }
        public string kuoni_Address1Floor { get; set; }
        public string kuoni_Address1HouseNumber { get; set; }
        public string kuoni_Address1HouseNumberAddition { get; set; }
        public Nullable<int> kuoni_Address1Reason { get; set; }
        public Nullable<int> kuoni_Address1Status { get; set; }
        public string kuoni_Address1Street4 { get; set; }
        public string kuoni_Address1Street5 { get; set; }
        public string kuoni_Address1Street6 { get; set; }
        public string kuoni_Address1Street7 { get; set; }
        public string kuoni_Address2CareOf { get; set; }
        public string kuoni_Address2FlatNumber { get; set; }
        public string kuoni_Address2FlatSuffix { get; set; }
        public string kuoni_Address2Floor { get; set; }
        public string kuoni_Address2HouseNumber { get; set; }
        public string kuoni_Address2HouseNumberAddition { get; set; }
        public Nullable<int> kuoni_Address2Reason { get; set; }
        public Nullable<int> kuoni_Address2Status { get; set; }
        public string kuoni_Address2Street4 { get; set; }
        public string kuoni_Address2Street5 { get; set; }
        public string kuoni_Address2Street6 { get; set; }
        public string kuoni_Address2Street7 { get; set; }
        public string kuoni_Agency { get; set; }
        public Nullable<double> kuoni_AverageGroupSize { get; set; }
        public Nullable<int> kuoni_BookingsCount { get; set; }
        public Nullable<int> kuoni_BookingsCountAll { get; set; }
        public Nullable<int> kuoni_BookingsCountLast36Month { get; set; }
        public Nullable<bool> kuoni_BookingsCreatedPrevious2Years { get; set; }
        public Nullable<bool> kuoni_BookingsCreatedThisYear { get; set; }
        public Nullable<bool> kuoni_CaseExists { get; set; }
        public Nullable<bool> kuoni_CriticalContact { get; set; }
        public string kuoni_CriticalContactReason { get; set; }
        public string kuoni_CustomerCrmId { get; set; }
        public Nullable<int> kuoni_CustomerValue { get; set; }
        public Nullable<System.DateTime> kuoni_DateOfMerge { get; set; }
        public Nullable<bool> kuoni_DoNotSms { get; set; }
        public Nullable<System.DateTime> kuoni_ExpiryDate { get; set; }
        public string kuoni_GroupCode { get; set; }
        public string kuoni_Info01 { get; set; }
        public string kuoni_Info02 { get; set; }
        public string kuoni_Info03 { get; set; }
        public string kuoni_Info04 { get; set; }
        public string kuoni_Info05 { get; set; }
        public string kuoni_Info06 { get; set; }
        public string kuoni_Info07 { get; set; }
        public string kuoni_Info08 { get; set; }
        public string kuoni_Info09 { get; set; }
        public string kuoni_Info10 { get; set; }
        public string kuoni_Info11 { get; set; }
        public string kuoni_Info12 { get; set; }
        public string kuoni_Info13 { get; set; }
        public string kuoni_Info14 { get; set; }
        public string kuoni_Info15 { get; set; }
        public string kuoni_Info16 { get; set; }
        public string kuoni_Initials { get; set; }
        public Nullable<System.DateTime> kuoni_IssueDate { get; set; }
        public Nullable<int> kuoni_Language { get; set; }
        public Nullable<System.DateTime> kuoni_LastDateCampaignResponseReceived { get; set; }
        public Nullable<System.DateTime> kuoni_LastTimeCalculated { get; set; }
        public Nullable<System.DateTime> kuoni_LatestCreationDateOfCase { get; set; }
        public Nullable<int> kuoni_LeadCategory { get; set; }
        public string kuoni_LegacyCreatedBy { get; set; }
        public Nullable<System.DateTime> kuoni_LegacyCreatedOn { get; set; }
        public string kuoni_LegacyId { get; set; }
        public string kuoni_LegacyModifiedBy { get; set; }
        public Nullable<System.DateTime> kuoni_LegacyModifiedOn { get; set; }
        public Nullable<bool> kuoni_MarkedForDeletion { get; set; }
        public Nullable<int> kuoni_MarketingDiscount { get; set; }
        public Nullable<int> kuoni_MarketingDiscountLast36Month { get; set; }
        public Nullable<bool> kuoni_Merged { get; set; }
        public string kuoni_MosaicType { get; set; }
        public string kuoni_Nationality { get; set; }
        public Nullable<System.DateTime> kuoni_NextBirthday { get; set; }
        public Nullable<int> kuoni_Nps { get; set; }
        public Nullable<System.DateTime> kuoni_NpsDate { get; set; }
        public Nullable<int> kuoni_Occupation { get; set; }
        public Nullable<System.DateTime> kuoni_PassportExpiryDate { get; set; }
        public Nullable<System.DateTime> kuoni_PassportIssueDate { get; set; }
        public string kuoni_PassportNumber { get; set; }
        public string kuoni_PhoneNumber1 { get; set; }
        public string kuoni_PhoneNumber2 { get; set; }
        public string kuoni_PhoneNumber3 { get; set; }
        public Nullable<int> kuoni_PhoneType1 { get; set; }
        public Nullable<int> kuoni_PhoneType2 { get; set; }
        public Nullable<int> kuoni_PhoneType3 { get; set; }
        public string kuoni_PlaceOfBirth { get; set; }
        public string kuoni_PlaceOfIssue { get; set; }
        public Nullable<int> kuoni_QuotesCount { get; set; }
        public Nullable<double> kuoni_QuotesToBooking { get; set; }
        public string kuoni_SalesChannel { get; set; }
        public Nullable<bool> kuoni_SalesProcessBooker { get; set; }
        public Nullable<bool> kuoni_SalesProcessComplainant { get; set; }
        public Nullable<bool> kuoni_SalesProcessTravelling { get; set; }
        public string kuoni_Segment { get; set; }
        public Nullable<System.DateTime> kuoni_SegmentTime { get; set; }
        public Nullable<int> kuoni_SegmentType { get; set; }
        public string kuoni_ToCode { get; set; }
        public Nullable<decimal> kuoni_TotalAmountBookingsLast36Month { get; set; }
        public Nullable<decimal> kuoni_totalamountbookingslast36month_Base { get; set; }
        public Nullable<decimal> kuoni_TotalGrossAmount { get; set; }
        public Nullable<decimal> kuoni_totalgrossamount_Base { get; set; }
        public Nullable<decimal> kuoni_TotalNetAmount { get; set; }
        public Nullable<decimal> kuoni_totalnetamount_Base { get; set; }
        public Nullable<decimal> kuoni_TotalRevenue { get; set; }
        public Nullable<decimal> kuoni_totalrevenue_Base { get; set; }
        public Nullable<bool> kuoni_UpdatedByLegacySystem { get; set; }
        public Nullable<bool> kuoni_WinningRecord { get; set; }
        public string kuoni_XProfilerId { get; set; }
        public Nullable<System.Guid> kuoni_AgencyCreatedById { get; set; }
        public Nullable<System.Guid> kuoni_AgencyModifiedById { get; set; }
        public Nullable<System.Guid> kuoni_ParentAgencyId { get; set; }
        public Nullable<System.Guid> kuoni_CurrencyId { get; set; }
        public Nullable<System.Guid> kuoni_AgentCreatedById { get; set; }
        public Nullable<System.Guid> kuoni_AgentModifiedById { get; set; }
        public Nullable<System.Guid> kuoni_OriginOfSourceId { get; set; }
        public Nullable<System.Guid> kuoni_SourceSystemId { get; set; }
        public Nullable<System.Guid> kuoni_SubstitutionId { get; set; }
        public Nullable<System.Guid> kuoni_ResponsibleId { get; set; }
        public string kuoni_EMailSalutation { get; set; }
        public Nullable<bool> kuoni_HolidayActivityBiking { get; set; }
        public Nullable<bool> kuoni_HolidayActivityCanoeKajak { get; set; }
        public Nullable<bool> kuoni_HolidayActivityClimbingParagliding { get; set; }
        public Nullable<bool> kuoni_HolidayActivityDivingSnorkeling { get; set; }
        public Nullable<bool> kuoni_HolidayActivityFishing { get; set; }
        public Nullable<bool> kuoni_HolidayActivityFitness { get; set; }
        public Nullable<bool> kuoni_HolidayActivityFootballSoccer { get; set; }
        public Nullable<bool> kuoni_HolidayActivityGolf { get; set; }
        public Nullable<bool> kuoni_HolidayActivityHorsebackRiding { get; set; }
        public Nullable<bool> kuoni_HolidayActivityMotorbiking { get; set; }
        public Nullable<bool> kuoni_HolidayActivityMountainbiking { get; set; }
        public Nullable<bool> kuoni_HolidayActivityPolo { get; set; }
        public Nullable<bool> kuoni_HolidayActivityRunningMarathon { get; set; }
        public Nullable<bool> kuoni_HolidayActivitySailing { get; set; }
        public Nullable<bool> kuoni_HolidayActivitySportingEventsParticipant { get; set; }
        public Nullable<bool> kuoni_HolidayActivitySurfingWindsurfingKiteSur { get; set; }
        public Nullable<bool> kuoni_HolidayActivityTennisSquash { get; set; }
        public Nullable<bool> kuoni_HolidayActivityTrekking { get; set; }
        public Nullable<bool> kuoni_HolidayActivityWalkingHiking { get; set; }
        public Nullable<bool> kuoni_HolidayActivityWinterSports { get; set; }
        public Nullable<bool> kuoni_HolidayCategoryAccomodationonly { get; set; }
        public Nullable<bool> kuoni_HolidayCategoryAdventureExpeditionSafari { get; set; }
        public Nullable<bool> kuoni_HolidayCategoryApartmentHolidayHome { get; set; }
        public Nullable<bool> kuoni_HolidayCategoryBeachHoliday { get; set; }
        public Nullable<bool> kuoni_HolidayCategoryCityBreak { get; set; }
        public Nullable<bool> kuoni_HolidayCategoryClubAllInclusive { get; set; }
        public Nullable<bool> kuoni_HolidayCategoryCruise { get; set; }
        public Nullable<bool> kuoni_HolidayCategoryFlightonly { get; set; }
        public Nullable<bool> kuoni_HolidayCategoryMotorhomeCamping { get; set; }
        public Nullable<bool> kuoni_HolidayCategoryRiverCruise { get; set; }
        public Nullable<bool> kuoni_HolidayCategoryRoundtripFlyDrive { get; set; }
        public Nullable<bool> kuoni_HolidayCategoryStudyTrip { get; set; }
        public Nullable<bool> kuoni_HolidayExperienceCulture { get; set; }
        public Nullable<bool> kuoni_HolidayExperienceDanceBallet { get; set; }
        public Nullable<bool> kuoni_HolidayExperienceDisabled { get; set; }
        public Nullable<bool> kuoni_HolidayExperienceExcursions { get; set; }
        public Nullable<bool> kuoni_HolidayExperienceFloraFauna { get; set; }
        public Nullable<bool> kuoni_HolidayExperienceGayLesbian { get; set; }
        public Nullable<bool> kuoni_HolidayExperienceGourmetWine { get; set; }
        public Nullable<bool> kuoni_HolidayExperienceHoneymoon { get; set; }
        public Nullable<bool> kuoni_HolidayExperienceKids { get; set; }
        public Nullable<bool> kuoni_HolidayExperienceMuseumsExhibitions { get; set; }
        public Nullable<bool> kuoni_HolidayExperienceMusicClassicalOpera { get; set; }
        public Nullable<bool> kuoni_HolidayExperienceMusicContempMusical { get; set; }
        public Nullable<bool> kuoni_HolidayExperienceNightlifeParty { get; set; }
        public Nullable<bool> kuoni_HolidayExperienceShoppingLifestyle { get; set; }
        public Nullable<bool> kuoni_HolidayExperienceSingle { get; set; }
        public Nullable<bool> kuoni_HolidayExperienceSportingEventsParticip { get; set; }
        public Nullable<bool> kuoni_HolidayExperienceSportingEventsSpectator { get; set; }
        public Nullable<bool> kuoni_HolidayExperienceSustainableEthical { get; set; }
        public Nullable<bool> kuoni_HolidayExperienceTheatreLiterature { get; set; }
        public Nullable<bool> kuoni_HolidayExperienceWellnessSpaHealth { get; set; }
        public string kuoni_LetterSalutation { get; set; }
        public Nullable<int> kuoni_travelplanfavoriteregion { get; set; }
        public Nullable<int> kuoni_TravelPlansNextRunningEvent { get; set; }
        public Nullable<bool> kuoni_TravelPreferenceCar { get; set; }
        public Nullable<bool> kuoni_TravelPreferenceLonghaulFlight { get; set; }
        public Nullable<bool> kuoni_TravelPreferenceMidhaulFlight { get; set; }
        public Nullable<bool> kuoni_TravelPreferenceSelfDriveCarRental { get; set; }
        public Nullable<bool> kuoni_TravelPreferenceShorthaulFlight { get; set; }
        public Nullable<bool> kuoni_TravelPreferenceTrain { get; set; }
        public Nullable<bool> kuoni_TravelStructureGroupescorted { get; set; }
        public Nullable<bool> kuoni_TravelStructureIndividual { get; set; }
        public Nullable<bool> kuoni_TravelStructurePackage { get; set; }
        public string kuoni_AdditionalInformationRemarks { get; set; }
        public Nullable<int> kuoni_CriticalContactType { get; set; }
        public Nullable<System.Guid> kuoni_NextOrderId { get; set; }
        public Nullable<System.DateTime> kuoni_BirthdayHolidayReminderDate { get; set; }
        public Nullable<System.DateTime> kuoni_CustomerReactivationTaskOn { get; set; }
        public Nullable<bool> kuoni_HolidayExperienceSportingEvents { get; set; }
        public Nullable<System.DateTime> kuoni_HolidayReminderTaskOn { get; set; }
        public Nullable<System.DateTime> kuoni_SalesCallCreatedOn { get; set; }
        public Nullable<int> kuoni_Age { get; set; }
        public Nullable<decimal> kuoni_AverageSpentPerBookingAmount { get; set; }
        public Nullable<decimal> kuoni_averagespentperbookingamount_Base { get; set; }
        public Nullable<decimal> kuoni_AverageSpentPerPersonAmount { get; set; }
        public Nullable<decimal> kuoni_averagespentperpersonamount_Base { get; set; }
        public Nullable<int> kuoni_CampaignsCountLast12Months { get; set; }
        public Nullable<int> kuoni_CampaignsCountLast2Months { get; set; }
        public Nullable<bool> kuoni_EMailContactable { get; set; }
        public Nullable<bool> kuoni_LetterContactable { get; set; }
        public Nullable<bool> kuoni_NewsletterSubscriber { get; set; }
        public Nullable<decimal> kuoni_AveragePricePerPersonAmount { get; set; }
        public string kuoni_MatchCodeFirstNameLastNameZipPlace { get; set; }
        public Nullable<System.Guid> kuoni_OpenCaseId { get; set; }
        public Nullable<System.DateTime> kuoni_LastSurveySentOn { get; set; }
        public Nullable<bool> kuoni_Asia365Customer { get; set; }
        public Nullable<int> kuoni_DonotAllowSurvey { get; set; }
        public Nullable<bool> kuoni_KontikiActive { get; set; }
        public Nullable<bool> kuoni_KontikiCulture { get; set; }
        public Nullable<bool> kuoni_KontikiCustomer { get; set; }
        public Nullable<bool> kuoni_KontikiHero { get; set; }
        public Nullable<bool> kuoni_KontikiNatureObserver { get; set; }
        public Nullable<bool> kuoni_KuoniCruiseCustomer { get; set; }
        public Nullable<bool> kuoni_MantaCustomer { get; set; }
        public Nullable<bool> kuoni_MantaDiving { get; set; }
        public Nullable<bool> kuoni_MantaGolf { get; set; }
        public Nullable<bool> kuoni_MantaSnorkeling { get; set; }
        public Nullable<System.DateTime> kuoni_NPSSurveysenton { get; set; }
        public Nullable<bool> kuoni_PrivateSafariCustomer { get; set; }
        public Nullable<bool> kuoni_SendMarketingMaterialKontiki { get; set; }
        public Nullable<bool> kuoni_SendMarketingMaterialKuoni { get; set; }
        public Nullable<bool> kuoni_SendMarketingMaterialManta { get; set; }
        public string kuoni_YomiLastNameCity { get; set; }
        public string kuoni_YomiLastNamePostalCode { get; set; }
        public Nullable<bool> kuoni_HolidayActivityCrossCountrySkiing { get; set; }


        public const string EntityName = "Contact";
        public string UniqueIdentifier => (InnerGuid + "!!" + EntityName).ToUpper();

        public override string MainFieldsMessage
        {
            get { return string.Format("Login='{0}' and Name='{1}'", LastName, FullName); }
        }

        

    }
    [Serializable]
    public class Contacts : IImportEntityCollection<Contact>
    {

        private Contact[] contacts;
        public Contact[] Contact
        {
            get => contacts;
            set => contacts = value;
        }
        //public IEnumerable<Contact> Entitites => Contact;

        public IEnumerable<Contact> Entities => Contact;
    }
}
