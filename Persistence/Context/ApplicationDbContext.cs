﻿using System.Threading;
using HRRS.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Entities;

namespace HRRS.Persistence.Context;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> context) : base(context)
    {

    }

    public DbSet<User> Users { get; set; }
    public DbSet<Mapdanda> Mapdandas { get; set; }
    public DbSet<HospitalStandard> HospitalStandards { get; set; }
    public DbSet<HealthFacility> HealthFacilities { get; set; }
    public DbSet<TempHealthFacility> TempHealthFacilities { get; set; }
    public DbSet<Anusuchi> Anusuchis { get; set; }
    public DbSet<Parichhed> Parichheds { get; set; }
    public DbSet<SubParichhed> SubParichheds { get; set; }
    public DbSet<SubSubParichhed> SubSubParichheds { get; set; }
    public DbSet<SubMapdanda> SubMapdandas { get; set; }
    public DbSet<HospitalStandardEntry> HospitalStandardEntrys { get; set; }
    public DbSet<MasterStandardEntry> MasterStandardEntries { get; set; }
    public DbSet<SubmissionStatus> Approvals { get; set; }
    public DbSet<FacilityType> HospitalType { get; set; }
    public DbSet<Role> UserRoles { get; set; }
    public DbSet<Province> Provinces { get; set; }
    public DbSet<District> Districts { get; set; }
    public DbSet<LocalLevel> LocalLevels { get; set; }
    public DbSet<RegistrationRequest> RegistrationRequests { get; set; }
    public DbSet<SubmissionType> SubmissionTypes { get; set; }
    public DbSet<AnusuchiMapping> AnusuchiMappings { get; set; }
    public DbSet<AnusuchiMapdandaTableMapping> AnusuchiMapdandaTableMappings { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
        optionsBuilder.UseSeeding((context, _) =>
        {
            var user = context.Set<User>().FirstOrDefault();

            if (user is null)
            {

                var newUser = new User()
                {
                    UserName = "Administrator",
                    Password = BCrypt.Net.BCrypt.HashPassword("12345", 12),
                    UserType = "SuperAdmin",
                    DistrictId = 4,
                    ProvinceId = 1,
                    FacilityTypeId = 1,
                };
                context.Set<User>().Add(newUser);
                context.SaveChanges();
            }

            var otherAdmins = context.Set<User>().Where(x => x.UserType == "localadmin");
            if(!otherAdmins.Any())
            {
                var local1 = new User()
                {
                    UserName = "LocalAdmin",
                    Password = BCrypt.Net.BCrypt.HashPassword("12345", 12),
                    UserType = "localadmin",
                    DistrictId = 4,
                    ProvinceId = 1,
                    FacilityTypeId = 1,
                };

                var local2 = new User()
                {
                    UserName = "LocalAdmin1",
                    Password = BCrypt.Net.BCrypt.HashPassword("12345", 12),
                    UserType = "localadmin",
                    DistrictId = 4,
                    ProvinceId = 1,
                    FacilityTypeId = 1,
                };
                context.Set<User>().AddRange([local1, local2]);
                context.SaveChanges();
            }

            var submissionTypes = context.Set<SubmissionType>().FirstOrDefault();
            if(submissionTypes is null)
            {
                List<SubmissionType> list = [
                    new SubmissionType { Title = "नयाँ स्वास्थ्य संस्थाको दर्ता" },
                    new SubmissionType { Title = "नवीकरण" },
                    new SubmissionType { Title = "स्तरोन्नति" },
                    new SubmissionType { Title = "सेवा बिस्तार" },
                    new SubmissionType { Title = "शाखा बिस्तार" },
                    new SubmissionType { Title = "स्थानान्तरण" }
                ];

                context.Set<SubmissionType>().AddRange(list);
                context.SaveChanges();
            }

            var roles = context.Set<Role>().FirstOrDefault();
            if (roles is null)
            {
                var role1 = new Role()
                {
                    BedCount = 25,
                    Title = "Jilla",
                };

                var role2 = new Role()
                {
                    BedCount = 50,
                    Title = "Inrdesanalaya",
                };

                var role3 = new Role()
                {
                    BedCount = 100,
                    Title = "Mantralaya",
                };

                var role4 = new Role()
                {
                    BedCount = 200,
                    Title = "SuperAdmin",
                };

                context.Set<Role>().AddRange([role1, role2, role3, role4]);
                context.SaveChanges();

            }
            /*
            var provincesList = context.Set<Province>().FirstOrDefault();
            if (provincesList is null)
            {
                List<Province> provinces = [
                    new Province { Name = "Koshi" },
                    new Province { Name = "Madhesh Pradesh" },
                    new Province { Name = "Bagmati" },
                    new Province { Name = "Gandaki" },
                    new Province { Name = "Lumbini" },
                    new Province { Name = "Karnali" },
                    new Province { Name = "Sudurpaschim" }
                ];

                context.Set<Province>().AddRange(provinces);

                var districts = context.Set<District>().FirstOrDefault();
                if (districts is null)
                {
                    List<District> list = [
                        new District { Name = "Taplejung", Province = provinces[0] },
                        new District { Name = "Sankhuwasabha", Province = provinces[0] },
                        new District { Name = "Solukhumbu", Province = provinces[0] },
                        new District { Name = "Okhaldhunga", Province = provinces[0] },
                        new District { Name = "Khotang", Province = provinces[0] },
                        new District { Name = "Bhojpur", Province = provinces[0] },
                        new District { Name = "Dhankuta", Province = provinces[0] },
                        new District { Name = "Terhathum", Province = provinces[0] },
                        new District { Name = "Panchthar", Province = provinces[0] },
                        new District { Name = "Ilam", Province = provinces[0] },
                        new District { Name = "Jhapa", Province = provinces[0] },
                        new District { Name = "Morang", Province = provinces[0] },
                        new District { Name = "Sunsari", Province = provinces[0] },
                        new District { Name = "Udayapur", Province = provinces[0] },
                        new District { Name = "Saptari", Province = provinces[1] },
                        new District { Name = "Siraha", Province = provinces[1] },
                        new District { Name = "Dhanusa", Province = provinces[1] },
                        new District { Name = "Mahottari", Province = provinces[1] },
                        new District { Name = "Sarlahi", Province = provinces[1] },
                        new District { Name = "Rautahat", Province = provinces[1] },
                        new District { Name = "Bara", Province = provinces[1] },
                        new District { Name = "Parsa", Province = provinces[1] },
                        new District { Name = "Dolakha", Province = provinces[2] },
                        new District { Name = "Sindhupalchok", Province = provinces[2] },
                        new District { Name = "Rasuwa", Province = provinces[2] },
                        new District { Name = "Dhading", Province = provinces[2] },
                        new District { Name = "Nuwakot", Province = provinces[2] },
                        new District { Name = "Kathmandu", Province = provinces[2] },
                        new District { Name = "Bhaktapur", Province = provinces[2] },
                        new District { Name = "Lalitpur", Province = provinces[2] },
                        new District { Name = "Kavrepalanchok", Province = provinces[2] },
                        new District { Name = "Ramechhap", Province = provinces[2] },
                        new District { Name = "Sindhuli", Province = provinces[2] },
                        new District { Name = "Makwanpur", Province = provinces[2] },
                        new District { Name = "Chitawan", Province = provinces[2] },
                        new District { Name = "Gorkha", Province = provinces[3] },
                        new District { Name = "Manang", Province = provinces[3] },
                        new District { Name = "Myagdi", Province = provinces[3] },
                        new District { Name = "Kaski", Province = provinces[3] },
                        new District { Name = "Lamjung", Province = provinces[3] },
                        new District { Name = "Tanahu", Province = provinces[3] },
                        new District { Name = "Nawalparasi (Bardaghat Susta East)", Province = provinces[3] },
                        new District { Name = "Syangja", Province = provinces[3] },
                        new District { Name = "Parbat", Province = provinces[3] },
                        new District { Name = "Baglung", Province = provinces[3] },
                        new District { Name = "Rukum (East)", Province = provinces[4] },
                        new District { Name = "Rolpa", Province = provinces[4] },
                        new District { Name = "Pyuthan", Province = provinces[4] },
                        new District { Name = "Gulmi", Province = provinces[4] },
                        new District { Name = "Arghakhanchi", Province = provinces[4] },
                        new District { Name = "Palpa", Province = provinces[4] },
                        new District { Name = "Nawalparasi (Bardaghat Susta West)", Province = provinces[4] },
                        new District { Name = "Rupandehi", Province = provinces[4] },
                        new District { Name = "Kapilbastu", Province = provinces[4] },
                        new District { Name = "Dang", Province = provinces[4] },
                        new District { Name = "Banke", Province = provinces[4] },
                        new District { Name = "Bardiya", Province = provinces[4] },
                        new District { Name = "Dolpa", Province = provinces[4] },
                        new District { Name = "Mugu", Province = provinces[4] },
                        new District { Name = "Humla", Province = provinces[4] },
                        new District { Name = "Jumla", Province = provinces[4] },
                        new District { Name = "Kalikot", Province = provinces[4] },
                        new District { Name = "Dailekh", Province = provinces[4] },
                        new District { Name = "Jajarkot", Province = provinces[4] },
                        new District { Name = "Rukum (West)", Province = provinces[4] },
                        new District { Name = "Salyan", Province = provinces[4] },
                        new District { Name = "Surkhet", Province = provinces[4] },
                        new District { Name = "Bajura", Province = provinces[5] },
                        new District { Name = "Bajhang", Province = provinces[5] },
                        new District { Name = "Darchula", Province = provinces[5] },
                        new District { Name = "Baitadi", Province = provinces[5] },
                        new District { Name = "Dadeldhura", Province = provinces[5] },
                        new District { Name = "Doti", Province = provinces[5] },
                        new District { Name = "Achham", Province = provinces[5] },
                        new District { Name = "Kailali", Province = provinces[5] },
                        new District { Name = "Kanchanpur", Province = provinces[5] }
                        
                    ];
                   context.Set<District>().AddRange(list);
                }
                context.SaveChanges();
            }
            */

            List<Anusuchi> anusuchis = [
                    new() { DafaNo = "दफा ३ सँग सम्बन्धित", Name = "नयाँ स्वास्थ्य संस्थाको सेवा सञ्चालन पूर्वाधार स्वीकृतिको आशय पत्र (लेटर अफ इन्टेन्ट) प्रदान गर्नका लागि वस्तुगत मापदण्ड", SerialNo = "1" },
                    new() { DafaNo = "दफा ३ सँग सम्बन्धित", Name = "स्वास्थ्य संस्थाको स्तर निर्धारण नीतिका लागि पूर्वाधार स्वीकृतिको आशय पत्र (लेटर अफ इन्टेन्ट) प्रदान गर्नका लागि वस्तुगत मापदण्ड", SerialNo = "2" },
                    new() { DafaNo = "दफा ४ सँग सम्बन्धित", Name = "स्वास्थ्य संस्थाको वैधानिक व्यवस्था सम्बन्धी मापदण्ड", SerialNo = "3" },
                ];

            var anusuchi = context.Set<Anusuchi>().FirstOrDefault();
            if (anusuchi is null)
            {
                
                context.Set<Anusuchi>().AddRange(anusuchis);
                context.SaveChanges();
            }

            var mapdanda = context.Set<Mapdanda>().FirstOrDefault();
            if (mapdanda is null)
            {
                List<Mapdanda> mapdandas = [
                    new()  {  SerialNumber = "1", Name = "स्वास्थ्य संस्थाको व्यवसाय दर्ता भएको प्रमाणपत्रको प्रतिलिपि", Anusuchi = anusuchis[0] },
                    new()  {  SerialNumber = "2", Name = "स्वास्थ्य संस्थाको प्रबन्धपत्र र नियमावलीको प्रतिलिपि", Anusuchi = anusuchis[0] },
                    new()  {  SerialNumber = "3", Name = "स्वास्थ्य संस्थाको स्थायी लेखा नम्बर प्रमाणपत्र", Anusuchi = anusuchis[0] },
                    new()  {  SerialNumber = "4", Name = "स्वास्थ्य संस्थाको कर चुक्ता (अघिल्लो आ.व. सम्मको) प्रमाणपत्र वा कर विवरण बुझाएको रसिद (नयाँको हकमा आवश्यक नहुने)", Anusuchi = anusuchis[0] },
                    new()  {  SerialNumber = "5", Name = "स्वास्थ्य संस्थाको गुरु योजना (Master Plan ) र कार्य योजना (Business Plan)", Anusuchi = anusuchis[0] },
                    new()  {  SerialNumber = "6", Name = "प्रदेश स्वास्थ्य सेवा ऐन, २०७५ तथा प्रदेश स्वास्थ्य सेवा नियमावली, २०७६ बमोजिम जग्गाको मापदण्ड पुरा भएको", Anusuchi = anusuchis[0] },
                    new()  {  SerialNumber = "7", Name = "स्वास्थ्य संस्था रहने भवनको नक्सा र नक्सा पास प्रमाणपत्र तथा जग्गा वा घर भाडा/लिजको हकमा वडा/पालिकाको रोहवरमा भएको सम्झौता पत्र", Anusuchi = anusuchis[0] },
                    new()  {  SerialNumber = "8", Name = "प्रदेशको वातावरण संरक्षण ऐन, २०७७ बमोजिम संक्षिप्त वातावरणीय परीक्षण (BES) वा प्रारम्भिक वातावरणीय परीक्षण (IEE) वा वातावरणीय प्रभाव मूल्याङ्कन (EIA) को प्रतिवेदन स्वीकृत भएको", Anusuchi = anusuchis[0] },
                    new()  {  SerialNumber = "9", Name = "स्वास्थ्य संस्था रहने स्थानको उपयुक्तता भएको", Anusuchi = anusuchis[0] },
                    new()  {   SerialNumber = "10", Name = "स्वास्थ्य संस्थाले सेवा पुर्याउने क्षेत्रको बृहतता र जनसंख्याको घनत्व घना/बाक्लो भएको", Anusuchi = anusuchis[0] },
                    new()  {   SerialNumber = "11", Name = "स्वास्थ्य संस्था वा अस्पताल स्थापना हुने स्थानमा सडक यातायातको पहुँच भएको", Anusuchi = anusuchis[0] },
                    new()  {   SerialNumber = "12", Name = "स्वास्थ्य संस्था वा अस्पतालको कुल सञ्‍चालक समितिको सदस्य मध्ये कम्तिमा दुई तिहाई सदस्य नेपाली नागरिक भएको", Anusuchi = anusuchis[0] },
                    new()  {   SerialNumber = "13", Name = "सम्बन्धित स्थानीय तहको सिफारिस पत्र", Anusuchi = anusuchis[0] },
                    new()  {   SerialNumber = "14", Name = "सम्बन्धित जनस्वास्थ्य कार्यालयको सिफारिस पत्र", Anusuchi = anusuchis[0] },

                    new()  {  SerialNumber = "1", Name = "स्वास्थ्य संस्थाको व्यवसाय दर्ता भएको प्रमाणपत्रको प्रतिलिपि", Anusuchi = anusuchis[1] },
                    new()  {  SerialNumber = "2", Name = "स्वास्थ्य संस्थाको प्रबन्धपत्र र नियमावलीको प्रतिलिपि", Anusuchi = anusuchis[1] },
                    new()  {  SerialNumber = "3", Name = "स्वास्थ्य संस्थाको स्थायी लेखा नम्बर प्रमाणपत्र", Anusuchi = anusuchis[1] },
                    new()  {  SerialNumber = "4", Name = "स्वास्थ्य संस्थाको नवीकरण भएको", Anusuchi = anusuchis[1] },
                    new()  {  SerialNumber = "5", Name = "स्वास्थ्य सेवा सञ्‍चालन अनुमति पत्र", Anusuchi = anusuchis[1] },
                    new()  {  SerialNumber = "6", Name = "हालको स्वास्थ्य सेवा सञ्‍चालनको नवीकरण भएको", Anusuchi = anusuchis[1] },
                    new()  {  SerialNumber = "7", Name = "स्वास्थ्य संस्थाको कर चुक्ता (अघिल्लो आ.व. सम्मको) प्रमाणपत्र वा कर विवरण बुझाएको रसिद (नयाँको हकमा आवश्यक नहुने)", Anusuchi = anusuchis[1] },
                    new()  {  SerialNumber = "8", Name = "स्वास्थ्य संस्थाको गुरु योजना (Master Plan) र कार्य योजना (Business Plan)", Anusuchi = anusuchis[1] },
                    new()  {  SerialNumber = "9", Name = "प्रदेश स्वास्थ्य सेवा ऐन, २०७५ तथा प्रदेश स्वास्थ्य सेवा नियमावली, २०७६ बमोजिम जग्गाको मापदण्ड पुरा भएको", Anusuchi = anusuchis[1] },
                    new()  {   SerialNumber = "10", Name = "स्वास्थ्य संस्था रहने भवनको नक्सा र नक्सा पास प्रमाणपत्र तथा जग्गा वा घर भाडा/लिजको हकमा सम्झौता पत्र", Anusuchi = anusuchis[1] },
                    new()  {   SerialNumber = "11", Name = "प्रदेशको वातावरण संरक्षण ऐन, २०७७ बमोजिम संक्षिप्त वातावरणीय परीक्षण (BES) वा प्रारम्भिक वातावरणीय परीक्षण (IEE) वा वातावरणीय प्रभाव मूल्याङ्कन (EIA) को प्रतिवेदन स्वीकृत भएको", Anusuchi = anusuchis[1] },
                    new()  {   SerialNumber = "12", Name = "स्वास्थ्य संस्थाको आयकर प्रमाणपत्र वेबसाइट वा सबैले देख्‍ने ठाउँमा राखेको", Anusuchi = anusuchis[1] },
                    new()  {   SerialNumber = "13", Name = "स्वास्थ्य संस्थाको नागरिक बडापत्र (सेवा, प्रक्रिया, सुविधा र शुल्क सहितको विवरण) वेबसाइट र सबैले देख्‍ने ठाउँमा प्रदर्शनमा राखेको", Anusuchi = anusuchis[1] },
                    new()  {   SerialNumber = "14", Name = "स्वास्थ्य संस्थाले प्रचलित कानूनमा तोकिए बमोजिम स्वास्थ्य औजार, मेशिन र उपकरण खरिद गरेको", Anusuchi = anusuchis[1] },
                    new()  {   SerialNumber = "15", Name = "सरकारले तोकिएको ढाँचामा (DHIS/AHMIS अनुसार) मासिक रुपमा प्रतिवेदन प्रविष्ट गर्ने वा जनस्वास्थ्य कार्यालयमा बुझाउने गरेको", Anusuchi = anusuchis[1] },
                    new()  {   SerialNumber = "16", Name = "जनस्वास्थ्य कार्यालय र सम्बन्धित स्थानीय तहको सिफारिस पत्र", Anusuchi = anusuchis[1] },
                    new()  {   SerialNumber = "17", Name = "गत आ.व. सम्मको लेखापरीक्षण प्रतिवेदन", Anusuchi = anusuchis[1] },
                    new()  {   SerialNumber = "18", Name = "स्वमूल्याङ्कन फाराम भरी प्रतिवेदन पेश गरेको र दस्तुर तिरेको रसिद/निस्सा पेश गरिएको", Anusuchi = anusuchis[1] },
                    new()  {   SerialNumber = "19", Name = "अस्पताल स्थापना हुने स्थानमा सडक यातायातको पहुँच भएको", Anusuchi = anusuchis[1] },
                    new()  {   SerialNumber = "20", Name = "बेवारिसे, अति गरिब, विपन्‍न, अपाङ्गता भएका विपन्‍न र असहाय बिरामीका लागि कुल शय्याको १०% शय्या छुट्याई उपलब्ध सेवा, औषधी तथा उपचार खर्च व्यहोर्ने गरेको", Anusuchi = anusuchis[1] },
                    new()  {   SerialNumber = "21", Name = "स्वास्थ्य संस्था वा अस्पतालको कूल सञ्‍चालक समितिको सदस्य मध्ये कम्तिमा दुई तिहाई सदस्य नेपाली नागरिक भएको", Anusuchi = anusuchis[1] },

                    new()  { SerialNumber = "1", Name = "स्वास्थ्य संस्था दर्ता भएको प्रमाणपत्र", Anusuchi = anusuchis[2] },
                    new()  { SerialNumber = "2", Name = "स्वास्थ्य संस्थाको स्थायी लेखा नम्बर प्रमाणपत्र", Anusuchi = anusuchis[2] },
                    new()  { SerialNumber = "3", Name = "स्थायी लेखा/आयकर प्रमाणपत्र सबैले देख्‍ने ठाउँमा राखिएको", Anusuchi = anusuchis[2] },
                    new()  { SerialNumber = "4", Name = "स्वास्थ्य संस्थाको प्रबन्धपत्र/विधान/नियमावलीको प्रतिलिपि", Anusuchi = anusuchis[2] },
                    new()  { SerialNumber = "5", Name = "स्वास्थ्य संस्थाको नवीकरण भएको (नयाँको हकमा आवश्यक नपर्ने)", Anusuchi = anusuchis[2] },
                    new()  { SerialNumber = "6", Name = "स्वास्थ्य सेवा सञ्‍चालन अनुमति पत्र (नयाँ सञ्‍चालन अनुमतिको हकमा आशय पत्रको प्रमाणपत्र)", Anusuchi = anusuchis[2] },
                    new()  { SerialNumber = "7", Name = "अघिल्लो आ.व. सम्मको स्वास्थ्य संस्थाको कर चुक्ता प्रमाणपत्र (नयाँको हकमा आवश्यक नपर्ने)", Anusuchi = anusuchis[2] },
                    new()  { SerialNumber = "8", Name = "स्वास्थ्य संस्थाको नागरिक बडापत्र (सेवा, प्रक्रिया, सुविधा र शुल्क सहितको विवरण) वेबसाइट र सबैले देख्‍ने ठाउँमा प्रदर्शनमा राखेको", Anusuchi = anusuchis[2] },
                    new()  { SerialNumber = "9", Name = "प्रदेश वातावरण संरक्षण ऐन २०७७ बमोजिम संक्षिप्त वातावरणीय परीक्षण (BES) वा प्रारम्भिक वातावरणीय परीक्षण (IEE) वा वातावरणीय प्रभाव मूल्याङ्कन (EIA) को स्वीकृति पत्र", Anusuchi = anusuchis[2] },
                    new()  { SerialNumber = "10", Name = "सरकारले तोकिएको ढाँचामा (HMIS /AHMIS अनुसार) मासिक रुपमा प्रतिवेदन प्रविष्ट गर्ने/बुझाउने गरेको (नयाँको हकमा आवश्यक नपर्ने)", Anusuchi = anusuchis[2] },
                    new()  { SerialNumber = "11", Name = "स्वास्थ्य संस्था भवनको निर्माण सम्पन्‍न प्रमाणपत्र (आशयपत्रको हकमा आवश्यक नपर्ने)", Anusuchi = anusuchis[2] },
                    new()  { SerialNumber = "12", Name = "गत आ.व. को वार्षिक लेखा परीक्षण प्रतिवेदन (नयाँको हकमा आवश्यक नपर्ने)", Anusuchi = anusuchis[2] },
                    new()  { SerialNumber = "13", Name = "स्वास्थ्य संस्थाको कार्य योजना (Business Plan)", Anusuchi = anusuchis[2] },
                    new()  { SerialNumber = "14", Name = "स्वास्थ्य संस्थाको आफ्नै वेवसाइट (अद्यावधिक गरिएको) भएको", Anusuchi = anusuchis[2] },
                    new()  { SerialNumber = "15", Name = "सम्बन्धित स्थानीय तहको सिफारिस पत्र", Anusuchi = anusuchis[2] },
                    new()  { SerialNumber = "16", Name = "सम्बन्धित जनस्वास्थ्य कार्यालयको सिफारिस पत्र", Anusuchi = anusuchis[2] },
                    new()  { SerialNumber = "17", Name = "बेवारिसे, अति गरिब, विपन्‍न, अपाङ्गता भएका विपन्‍न र असहाय बिरामीका लागि कूल शय्याको १०% शय्या छुट्याई उपलब्ध सेवा, औषधी तथा उपचार खर्च व्यहोर्ने गरेको प्रमाण/निस्सा (नयाँको हकमा आवश्यक नपर्ने)", Anusuchi = anusuchis[2] },
                    new()  { SerialNumber = "18", Name = "स्वास्थ्य संस्था वा अस्पतालको कूल सञ्‍चालक समितिको सदस्य मध्ये कम्तिमा दुई तिहाई सदस्य नेपाली नागरिक भएको", Anusuchi = anusuchis[2] }

                ];
                context.Set<Mapdanda>().AddRange(mapdandas);
                context.SaveChanges();
            }

        }).UseAsyncSeeding(async (context, _, cancellationToken) =>
        {
            var user = await context.Set<User>().FirstOrDefaultAsync(cancellationToken);
            if (user is null)
            {
                var newUser = new User()
                {
                    UserName = "Administrator",
                    Password = BCrypt.Net.BCrypt.HashPassword("12345", 12),
                    UserType = "SuperAdmin"

                };
                await context.Set<User>().AddAsync(newUser, cancellationToken);
                await context.SaveChangesAsync(cancellationToken);
            }

            var otherAdmins = context.Set<User>().Where(x => x.UserType == "localadmin");
            if (!await otherAdmins.AnyAsync(cancellationToken))
            {
                var local1 = new User()
                {
                    UserName = "LocalAdmin",
                    Password = BCrypt.Net.BCrypt.HashPassword("12345", 12),
                    UserType = "localadmin"
                };

                var local2 = new User()
                {
                    UserName = "LocalAdmin1",
                    Password = BCrypt.Net.BCrypt.HashPassword("12345", 12),
                    UserType = "localadmin"
                };
               await context.Set<User>().AddRangeAsync([local1, local2], cancellationToken);
               await context.SaveChangesAsync(cancellationToken);
            }

            var submissionTypes = await context.Set<SubmissionType>().FirstOrDefaultAsync(cancellationToken);
            if (submissionTypes is null)
            {
                List<SubmissionType> list = [
                    new SubmissionType { Title = "नयाँ स्वास्थ्य संस्थाको दर्ता" },
                    new SubmissionType { Title = "नवीकरण" },
                    new SubmissionType { Title = "स्तरोन्नति" },
                    new SubmissionType { Title = "सेवा बिस्तार" },
                    new SubmissionType { Title = "शाखा बिस्तार" },
                    new SubmissionType { Title = "स्थानान्तरण" }
                ];

                await context.Set<SubmissionType>().AddRangeAsync(list, cancellationToken);
                await context.SaveChangesAsync();
            }

            var roles = context.Set<Role>().FirstOrDefaultAsync(cancellationToken);
            if (roles is null)
            {
                var role1 = new Role()
                {
                    BedCount = 25,
                    Title = "Jilla",
                };

                var role2 = new Role()
                {
                    BedCount = 50,
                    Title = "Nirdesanalaya",
                };

                var role3 = new Role()
                {
                    BedCount = 100,
                    Title = "Mantraalaya",
                };

                var role4 = new Role()
                {
                    BedCount = 200,
                    Title = "SuperAdmin",
                };

                await context.Set<Role>().AddRangeAsync([role1, role2, role3, role4], cancellationToken);
                await context.SaveChangesAsync(cancellationToken);

            }

            /*var provincesList = await context.Set<Province>().FirstOrDefaultAsync(cancellationToken);
            if (provincesList is null)
            {
                List<Province> provinces = [
                    new Province { Name = "Koshi" },
                    new Province { Name = "Madhesh Pradesh" },
                    new Province { Name = "Bagmati" },
                    new Province { Name = "Gandaki" },
                    new Province { Name = "Lumbini" },
                    new Province { Name = "Karnali" },
                    new Province { Name = "Sudurpaschim" }
                ];

                await context.Set<Province>().AddRangeAsync(provinces, cancellationToken);

                var districts = await context.Set<District>().FirstOrDefaultAsync(cancellationToken);
                if (districts is null)
                {
                    List<District> list = [
                        new District { Name = "Taplejung", Province = provinces[0] },
                        new District { Name = "Sankhuwasabha", Province = provinces[0] },
                        new District { Name = "Solukhumbu", Province = provinces[0] },
                        new District { Name = "Okhaldhunga", Province = provinces[0] },
                        new District { Name = "Khotang", Province = provinces[0] },
                        new District { Name = "Bhojpur", Province = provinces[0] },
                        new District { Name = "Dhankuta", Province = provinces[0] },
                        new District { Name = "Terhathum", Province = provinces[0] },
                        new District { Name = "Panchthar", Province = provinces[0] },
                        new District { Name = "Ilam", Province = provinces[0] },
                        new District { Name = "Jhapa", Province = provinces[0] },
                        new District { Name = "Morang", Province = provinces[0] },
                        new District { Name = "Sunsari", Province = provinces[0] },
                        new District { Name = "Udayapur", Province = provinces[0] },
                        new District { Name = "Saptari", Province = provinces[1] },
                        new District { Name = "Siraha", Province = provinces[1] },
                        new District { Name = "Dhanusa", Province = provinces[1] },
                        new District { Name = "Mahottari", Province = provinces[1] },
                        new District { Name = "Sarlahi", Province = provinces[1] },
                        new District { Name = "Rautahat", Province = provinces[1] },
                        new District { Name = "Bara", Province = provinces[1] },
                        new District { Name = "Parsa", Province = provinces[1] },
                        new District { Name = "Dolakha", Province = provinces[2] },
                        new District { Name = "Sindhupalchok", Province = provinces[2] },
                        new District { Name = "Rasuwa", Province = provinces[2] },
                        new District { Name = "Dhading", Province = provinces[2] },
                        new District { Name = "Nuwakot", Province = provinces[2] },
                        new District { Name = "Kathmandu", Province = provinces[2] },
                        new District { Name = "Bhaktapur", Province = provinces[2] },
                        new District { Name = "Lalitpur", Province = provinces[2] },
                        new District { Name = "Kavrepalanchok", Province = provinces[2] },
                        new District { Name = "Ramechhap", Province = provinces[2] },
                        new District { Name = "Sindhuli", Province = provinces[2] },
                        new District { Name = "Makwanpur", Province = provinces[2] },
                        new District { Name = "Chitawan", Province = provinces[2] },
                        new District { Name = "Gorkha", Province = provinces[3] },
                        new District { Name = "Manang", Province = provinces[3] },
                        new District { Name = "Myagdi", Province = provinces[3] },
                        new District { Name = "Kaski", Province = provinces[3] },
                        new District { Name = "Lamjung", Province = provinces[3] },
                        new District { Name = "Tanahu", Province = provinces[3] },
                        new District { Name = "Nawalparasi (Bardaghat Susta East)", Province = provinces[3] },
                        new District { Name = "Syangja", Province = provinces[3] },
                        new District { Name = "Parbat", Province = provinces[3] },
                        new District { Name = "Baglung", Province = provinces[3] },
                        new District { Name = "Rukum (East)", Province = provinces[4] },
                        new District { Name = "Rolpa", Province = provinces[4] },
                        new District { Name = "Pyuthan", Province = provinces[4] },
                        new District { Name = "Gulmi", Province = provinces[4] },
                        new District { Name = "Arghakhanchi", Province = provinces[4] },
                        new District { Name = "Palpa", Province = provinces[4] },
                        new District { Name = "Nawalparasi (Bardaghat Susta West)", Province = provinces[4] },
                        new District { Name = "Rupandehi", Province = provinces[4] },
                        new District { Name = "Kapilbastu", Province = provinces[4] },
                        new District { Name = "Dang", Province = provinces[4] },
                        new District { Name = "Banke", Province = provinces[4] },
                        new District { Name = "Bardiya", Province = provinces[4] },
                        new District { Name = "Dolpa", Province = provinces[4] },
                        new District { Name = "Mugu", Province = provinces[4] },
                        new District { Name = "Humla", Province = provinces[4] },
                        new District { Name = "Jumla", Province = provinces[4] },
                        new District { Name = "Kalikot", Province = provinces[4] },
                        new District { Name = "Dailekh", Province = provinces[4] },
                        new District { Name = "Jajarkot", Province = provinces[4] },
                        new District { Name = "Rukum (West)", Province = provinces[4] },
                        new District { Name = "Salyan", Province = provinces[4] },
                        new District { Name = "Surkhet", Province = provinces[4] },
                        new District { Name = "Bajura", Province = provinces[5] },
                        new District { Name = "Bajhang", Province = provinces[5] },
                        new District { Name = "Darchula", Province = provinces[5] },
                        new District { Name = "Baitadi", Province = provinces[5] },
                        new District { Name = "Dadeldhura", Province = provinces[5] },
                        new District { Name = "Doti", Province = provinces[5] },
                        new District { Name = "Achham", Province = provinces[5] },
                        new District { Name = "Kailali", Province = provinces[5] },
                        new District { Name = "Kanchanpur", Province = provinces[5] }

                    ];
                    await context.Set<District>().AddRangeAsync(list, cancellationToken);
                }
                await context.SaveChangesAsync();
            }
            */

            List<Anusuchi> anusuchis = [
                new() { DafaNo = "दफा ३ सँग सम्बन्धित", Name = "नयाँ स्वास्थ्य संस्थाको सेवा सञ्चालन पूर्वाधार स्वीकृतिको आशय पत्र (लेटर अफ इन्टेन्ट) प्रदान गर्नका लागि वस्तुगत मापदण्ड", SerialNo = "1" },
                new() { DafaNo = "दफा ३ सँग सम्बन्धित", Name = "स्वास्थ्य संस्थाको स्तर निर्धारण नीतिका लागि पूर्वाधार स्वीकृतिको आशय पत्र (लेटर अफ इन्टेन्ट) प्रदान गर्नका लागि वस्तुगत मापदण्ड", SerialNo = "2" },
                new() { DafaNo = "दफा ४ सँग सम्बन्धित", Name = "स्वास्थ्य संस्थाको वैधानिक व्यवस्था सम्बन्धी मापदण्ड", SerialNo = "3" },
            ];

            var anusuchi = await context.Set<Anusuchi>().FirstOrDefaultAsync(cancellationToken);
            if (anusuchi is null)
            {
                
                await context.Set<Anusuchi>().AddRangeAsync(anusuchis, cancellationToken);
                await context.SaveChangesAsync(cancellationToken);
            }

            var mapdanda = context.Set<Mapdanda>().FirstOrDefaultAsync(cancellationToken);
            if (mapdanda is null)
            {
                List<Mapdanda> mapdandas = [
                    new()  {  SerialNumber = "1", Name = "स्वास्थ्य संस्थाको व्यवसाय दर्ता भएको प्रमाणपत्रको प्रतिलिपि", Anusuchi = anusuchis[0] },
                    new()  {  SerialNumber = "2", Name = "स्वास्थ्य संस्थाको प्रबन्धपत्र र नियमावलीको प्रतिलिपि", Anusuchi = anusuchis[0] },
                    new()  {  SerialNumber = "3", Name = "स्वास्थ्य संस्थाको स्थायी लेखा नम्बर प्रमाणपत्र", Anusuchi = anusuchis[0] },
                    new()  {  SerialNumber = "4", Name = "स्वास्थ्य संस्थाको कर चुक्ता (अघिल्लो आ.व. सम्मको) प्रमाणपत्र वा कर विवरण बुझाएको रसिद (नयाँको हकमा आवश्यक नहुने)", Anusuchi = anusuchis[0] },
                    new()  {  SerialNumber = "5", Name = "स्वास्थ्य संस्थाको गुरु योजना (Master Plan ) र कार्य योजना (Business Plan)", Anusuchi = anusuchis[0] },
                    new()  {  SerialNumber = "6", Name = "प्रदेश स्वास्थ्य सेवा ऐन, २०७५ तथा प्रदेश स्वास्थ्य सेवा नियमावली, २०७६ बमोजिम जग्गाको मापदण्ड पुरा भएको", Anusuchi = anusuchis[0] },
                    new()  {  SerialNumber = "7", Name = "स्वास्थ्य संस्था रहने भवनको नक्सा र नक्सा पास प्रमाणपत्र तथा जग्गा वा घर भाडा/लिजको हकमा वडा/पालिकाको रोहवरमा भएको सम्झौता पत्र", Anusuchi = anusuchis[0] },
                    new()  {  SerialNumber = "8", Name = "प्रदेशको वातावरण संरक्षण ऐन, २०७७ बमोजिम संक्षिप्त वातावरणीय परीक्षण (BES) वा प्रारम्भिक वातावरणीय परीक्षण (IEE) वा वातावरणीय प्रभाव मूल्याङ्कन (EIA) को प्रतिवेदन स्वीकृत भएको", Anusuchi = anusuchis[0] },
                    new()  {  SerialNumber = "9", Name = "स्वास्थ्य संस्था रहने स्थानको उपयुक्तता भएको", Anusuchi = anusuchis[0] },
                    new()  {   SerialNumber = "10", Name = "स्वास्थ्य संस्थाले सेवा पुर्याउने क्षेत्रको बृहतता र जनसंख्याको घनत्व घना/बाक्लो भएको", Anusuchi = anusuchis[0] },
                    new()  {   SerialNumber = "11", Name = "स्वास्थ्य संस्था वा अस्पताल स्थापना हुने स्थानमा सडक यातायातको पहुँच भएको", Anusuchi = anusuchis[0] },
                    new()  {   SerialNumber = "12", Name = "स्वास्थ्य संस्था वा अस्पतालको कुल सञ्‍चालक समितिको सदस्य मध्ये कम्तिमा दुई तिहाई सदस्य नेपाली नागरिक भएको", Anusuchi = anusuchis[0] },
                    new()  {   SerialNumber = "13", Name = "सम्बन्धित स्थानीय तहको सिफारिस पत्र", Anusuchi = anusuchis[0] },
                    new()  {   SerialNumber = "14", Name = "सम्बन्धित जनस्वास्थ्य कार्यालयको सिफारिस पत्र", Anusuchi = anusuchis[0] },

                    new()  {  SerialNumber = "1", Name = "स्वास्थ्य संस्थाको व्यवसाय दर्ता भएको प्रमाणपत्रको प्रतिलिपि", Anusuchi = anusuchis[1] },
                    new()  {  SerialNumber = "2", Name = "स्वास्थ्य संस्थाको प्रबन्धपत्र र नियमावलीको प्रतिलिपि", Anusuchi = anusuchis[1] },
                    new()  {  SerialNumber = "3", Name = "स्वास्थ्य संस्थाको स्थायी लेखा नम्बर प्रमाणपत्र", Anusuchi = anusuchis[1] },
                    new()  {  SerialNumber = "4", Name = "स्वास्थ्य संस्थाको नवीकरण भएको", Anusuchi = anusuchis[1] },
                    new()  {  SerialNumber = "5", Name = "स्वास्थ्य सेवा सञ्‍चालन अनुमति पत्र", Anusuchi = anusuchis[1] },
                    new()  {  SerialNumber = "6", Name = "हालको स्वास्थ्य सेवा सञ्‍चालनको नवीकरण भएको", Anusuchi = anusuchis[1] },
                    new()  {  SerialNumber = "7", Name = "स्वास्थ्य संस्थाको कर चुक्ता (अघिल्लो आ.व. सम्मको) प्रमाणपत्र वा कर विवरण बुझाएको रसिद (नयाँको हकमा आवश्यक नहुने)", Anusuchi = anusuchis[1] },
                    new()  {  SerialNumber = "8", Name = "स्वास्थ्य संस्थाको गुरु योजना (Master Plan) र कार्य योजना (Business Plan)", Anusuchi = anusuchis[1] },
                    new()  {  SerialNumber = "9", Name = "प्रदेश स्वास्थ्य सेवा ऐन, २०७५ तथा प्रदेश स्वास्थ्य सेवा नियमावली, २०७६ बमोजिम जग्गाको मापदण्ड पुरा भएको", Anusuchi = anusuchis[1] },
                    new()  {   SerialNumber = "10", Name = "स्वास्थ्य संस्था रहने भवनको नक्सा र नक्सा पास प्रमाणपत्र तथा जग्गा वा घर भाडा/लिजको हकमा सम्झौता पत्र", Anusuchi = anusuchis[1] },
                    new()  {   SerialNumber = "11", Name = "प्रदेशको वातावरण संरक्षण ऐन, २०७७ बमोजिम संक्षिप्त वातावरणीय परीक्षण (BES) वा प्रारम्भिक वातावरणीय परीक्षण (IEE) वा वातावरणीय प्रभाव मूल्याङ्कन (EIA) को प्रतिवेदन स्वीकृत भएको", Anusuchi = anusuchis[1] },
                    new()  {   SerialNumber = "12", Name = "स्वास्थ्य संस्थाको आयकर प्रमाणपत्र वेबसाइट वा सबैले देख्‍ने ठाउँमा राखेको", Anusuchi = anusuchis[1] },
                    new()  {   SerialNumber = "13", Name = "स्वास्थ्य संस्थाको नागरिक बडापत्र (सेवा, प्रक्रिया, सुविधा र शुल्क सहितको विवरण) वेबसाइट र सबैले देख्‍ने ठाउँमा प्रदर्शनमा राखेको", Anusuchi = anusuchis[1] },
                    new()  {   SerialNumber = "14", Name = "स्वास्थ्य संस्थाले प्रचलित कानूनमा तोकिए बमोजिम स्वास्थ्य औजार, मेशिन र उपकरण खरिद गरेको", Anusuchi = anusuchis[1] },
                    new()  {   SerialNumber = "15", Name = "सरकारले तोकिएको ढाँचामा (DHIS/AHMIS अनुसार) मासिक रुपमा प्रतिवेदन प्रविष्ट गर्ने वा जनस्वास्थ्य कार्यालयमा बुझाउने गरेको", Anusuchi = anusuchis[1] },
                    new()  {   SerialNumber = "16", Name = "जनस्वास्थ्य कार्यालय र सम्बन्धित स्थानीय तहको सिफारिस पत्र", Anusuchi = anusuchis[1] },
                    new()  {   SerialNumber = "17", Name = "गत आ.व. सम्मको लेखापरीक्षण प्रतिवेदन", Anusuchi = anusuchis[1] },
                    new()  {   SerialNumber = "18", Name = "स्वमूल्याङ्कन फाराम भरी प्रतिवेदन पेश गरेको र दस्तुर तिरेको रसिद/निस्सा पेश गरिएको", Anusuchi = anusuchis[1] },
                    new()  {   SerialNumber = "19", Name = "अस्पताल स्थापना हुने स्थानमा सडक यातायातको पहुँच भएको", Anusuchi = anusuchis[1] },
                    new()  {   SerialNumber = "20", Name = "बेवारिसे, अति गरिब, विपन्‍न, अपाङ्गता भएका विपन्‍न र असहाय बिरामीका लागि कुल शय्याको १०% शय्या छुट्याई उपलब्ध सेवा, औषधी तथा उपचार खर्च व्यहोर्ने गरेको", Anusuchi = anusuchis[1] },
                    new()  {   SerialNumber = "21", Name = "स्वास्थ्य संस्था वा अस्पतालको कूल सञ्‍चालक समितिको सदस्य मध्ये कम्तिमा दुई तिहाई सदस्य नेपाली नागरिक भएको", Anusuchi = anusuchis[1] },

                    new()  { SerialNumber = "1", Name = "स्वास्थ्य संस्था दर्ता भएको प्रमाणपत्र", Anusuchi = anusuchis[2] },
                    new()  { SerialNumber = "2", Name = "स्वास्थ्य संस्थाको स्थायी लेखा नम्बर प्रमाणपत्र", Anusuchi = anusuchis[2] },
                    new()  { SerialNumber = "3", Name = "स्थायी लेखा/आयकर प्रमाणपत्र सबैले देख्‍ने ठाउँमा राखिएको", Anusuchi = anusuchis[2] },
                    new()  { SerialNumber = "4", Name = "स्वास्थ्य संस्थाको प्रबन्धपत्र/विधान/नियमावलीको प्रतिलिपि", Anusuchi = anusuchis[2] },
                    new()  { SerialNumber = "5", Name = "स्वास्थ्य संस्थाको नवीकरण भएको (नयाँको हकमा आवश्यक नपर्ने)", Anusuchi = anusuchis[2] },
                    new()  { SerialNumber = "6", Name = "स्वास्थ्य सेवा सञ्‍चालन अनुमति पत्र (नयाँ सञ्‍चालन अनुमतिको हकमा आशय पत्रको प्रमाणपत्र)", Anusuchi = anusuchis[2] },
                    new()  { SerialNumber = "7", Name = "अघिल्लो आ.व. सम्मको स्वास्थ्य संस्थाको कर चुक्ता प्रमाणपत्र (नयाँको हकमा आवश्यक नपर्ने)", Anusuchi = anusuchis[2] },
                    new()  { SerialNumber = "8", Name = "स्वास्थ्य संस्थाको नागरिक बडापत्र (सेवा, प्रक्रिया, सुविधा र शुल्क सहितको विवरण) वेबसाइट र सबैले देख्‍ने ठाउँमा प्रदर्शनमा राखेको", Anusuchi = anusuchis[2] },
                    new()  { SerialNumber = "9", Name = "प्रदेश वातावरण संरक्षण ऐन २०७७ बमोजिम संक्षिप्त वातावरणीय परीक्षण (BES) वा प्रारम्भिक वातावरणीय परीक्षण (IEE) वा वातावरणीय प्रभाव मूल्याङ्कन (EIA) को स्वीकृति पत्र", Anusuchi = anusuchis[2] },
                    new()  { SerialNumber = "10", Name = "सरकारले तोकिएको ढाँचामा (HMIS /AHMIS अनुसार) मासिक रुपमा प्रतिवेदन प्रविष्ट गर्ने/बुझाउने गरेको", Anusuchi = anusuchis[2] },
                    new()  { SerialNumber = "11", Name = "(नयाँको हकमा आवश्यक नपर्ने)", Anusuchi = anusuchis[2] },
                    new()  { SerialNumber = "12", Name = "स्वास्थ्य संस्था भवनको निर्माण सम्पन्‍न प्रमाणपत्र", Anusuchi = anusuchis[2] },
                    new()  { SerialNumber = "13", Name = "(आशयपत्रको हकमा आवश्यक नपर्ने)", Anusuchi = anusuchis[2] },
                    new()  { SerialNumber = "14", Name = "गत आ.व. को वार्षिक लेखा परीक्षण प्रतिवेदन (नयाँको हकमा आवश्यक नपर्ने)", Anusuchi = anusuchis[2] },
                    new()  { SerialNumber = "15", Name = "स्वास्थ्य संस्थाको कार्य योजना (Business Plan)", Anusuchi = anusuchis[2] },
                    new()  { SerialNumber = "16", Name = "स्वास्थ्य संस्थाको आफ्नै वेवसाइट (अद्यावधिक गरिएको) भएको", Anusuchi = anusuchis[2] },
                    new()  { SerialNumber = "17", Name = "सम्बन्धित स्थानीय तहको सिफारिस पत्र", Anusuchi = anusuchis[2] },
                    new()  { SerialNumber = "18", Name = "सम्बन्धित जनस्वास्थ्य कार्यालयको सिफारिस पत्र", Anusuchi = anusuchis[2] },
                    new()  { SerialNumber = "19", Name = "बेवारिसे, अति गरिब, विपन्‍न, अपाङ्गता भएका विपन्‍न र असहाय बिरामीका लागि कूल शय्याको १०% शय्या छुट्याई उपलब्ध सेवा, औषधी तथा उपचार खर्च व्यहोर्ने गरेको प्रमाण/निस्सा", Anusuchi = anusuchis[2] },
                    new()  { SerialNumber = "20", Name = "(नयाँको हकमा आवश्यक नपर्ने)", Anusuchi = anusuchis[2] },
                    new()  { SerialNumber = "21", Name = "स्वास्थ्य संस्था वा अस्पतालको कूल सञ्‍चालक समितिको सदस्य मध्ये कम्तिमा दुई तिहाई सदस्य नेपाली नागरिक भएको", Anusuchi = anusuchis[2] }

                ];
                await context.Set<Mapdanda>().AddRangeAsync(mapdandas, cancellationToken);
                await context.SaveChangesAsync(cancellationToken);
            }
        });
    

}

