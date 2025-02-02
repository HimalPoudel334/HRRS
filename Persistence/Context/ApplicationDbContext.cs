using System.Threading;
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
    public DbSet<Anusuchi> Anusuchis { get; set; }
    public DbSet<Parichhed> Parichheds { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Anusuchi -> Parichheds (One-to-Many)
        modelBuilder.Entity<Parichhed>()
            .HasOne(p => p.Anusuchi)
            .WithMany(a => a.Parichheds)
            .HasForeignKey(p => p.AnusuchiId)
            .OnDelete(DeleteBehavior.Cascade);

        // Anusuchi -> Mapdandas (One-to-Many)
        modelBuilder.Entity<Mapdanda>()
            .HasOne(m => m.Anusuchi)
            .WithMany(a => a.Mapdandas)
            .HasForeignKey(m => m.AnusuchiId)
            .OnDelete(DeleteBehavior.Cascade);

        // Parichhed -> Mapdandas (One-to-Many)
        modelBuilder.Entity<Mapdanda>()
            .HasOne(m => m.Parichhed)
            .WithMany(p => p.Mapdandas)
            .HasForeignKey(m => m.ParichhedId)
            .OnDelete(DeleteBehavior.Restrict);

        // Parichhed -> SubParichheds (Self-referencing One-to-Many)
        modelBuilder.Entity<Parichhed>()
            .HasMany(p => p.SubParichheds)
            .WithOne()
            .HasForeignKey(p => p.AnusuchiId)
            .OnDelete(DeleteBehavior.Restrict);

        // Parichhed -> SubParichhed (Self-referencing One-to-Many in Mapdanda)
        modelBuilder.Entity<Mapdanda>()
            .HasOne(m => m.SubParichhed)
            .WithMany()
            .HasForeignKey(m => m.SubParichhedId)
            .OnDelete(DeleteBehavior.Restrict);

        // Mapdanda -> SubMapdandas (Self-referencing One-to-Many)
        modelBuilder.Entity<Mapdanda>()
            .HasMany(m => m.SubMapdandas)
            .WithOne()
            .HasForeignKey(m => m.SubParichhedId)
            .OnDelete(DeleteBehavior.Restrict);
    }

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
                    UserType = "SuperAdmin"
                };
                context.Set<User>().Add(newUser);
                context.SaveChanges();
            }

            var anusuchi = context.Set<Anusuchi>().FirstOrDefault();
            if(anusuchi is null)
            {
                List<Anusuchi> anusuchis = [
                    new() { RelatedToDafaNo = "दफा ३ सँग सÌबिÆधत", AnusuchiName = "नयाँ ÖवाÖÃय सÖं थाको सेवा स¼ चालन पूवाªधार Öवीकृितको आशय पý (लेटर अफ इÆटेÆट) ÿदान गनªका लािग वÖतगुत मापदÁड" },
                    new() { RelatedToDafaNo = "दफा ३ सँग सÌबिÆधत", AnusuchiName = "ÖवाÖÃय सÖं थाको ÖतरोÆ नितका लािग पवूाªधार Öवीकृितको आशय पý (लेटर अफ इÆटेÆट) ÿदान गनªका लािग\r\nवÖतुगत मापदÁड" },
                    new() { RelatedToDafaNo = "दफा ४ सँग सÌबिÆधत", AnusuchiName = "ÖवाÖÃय सÖं थाको वैधािनक ÓयवÖथा सÌबÆधी मापदÁड" },
                ];

                context.Set<Anusuchi>().AddRange(anusuchis);
                context.SaveChanges();
            }


            var mapdanda = context.Set<Mapdanda>().FirstOrDefault();
            if (mapdanda is null)
            {
                List<Mapdanda> mapdandas = [
                    new()  {  SerialNumber = 1, Name = "स्वास्थ्य संस्थाको व्यवसाय दर्ता भएको प्रमाणपत्रको प्रतिलिपि", AnusuchiId = 1 },
                    new()  {  SerialNumber = 2, Name = "स्वास्थ्य संस्थाको प्रबन्धपत्र र नियमावलीको प्रतिलिपि", AnusuchiId = 1 },
                    new()  {  SerialNumber = 3, Name = "स्वास्थ्य संस्थाको स्थायी लेखा नम्बर प्रमाणपत्र", AnusuchiId = 1 },
                    new()  {  SerialNumber = 4, Name = "स्वास्थ्य संस्थाको कर चुक्ता (अघिल्लो आ.व. सम्मको) प्रमाणपत्र वा कर विवरण बुझाएको रसिद (नयाँको हकमा आवश्यक नहुने)", AnusuchiId = 1 },
                    new()  {  SerialNumber = 5, Name = "स्वास्थ्य संस्थाको गुरु योजना (Master Plan ) र कार्य योजना (Business Plan)", AnusuchiId = 1 },
                    new()  {  SerialNumber = 6, Name = "प्रदेश स्वास्थ्य सेवा ऐन, २०७५ तथा प्रदेश स्वास्थ्य सेवा नियमावली, २०७६ बमोजिम जग्गाको मापदण्ड पुरा भएको", AnusuchiId = 1 },
                    new()  {  SerialNumber = 7, Name = "स्वास्थ्य संस्था रहने भवनको नक्सा र नक्सा पास प्रमाणपत्र तथा जग्गा वा घर भाडा/लिजको हकमा वडा/पालिकाको रोहवरमा भएको सम्झौता पत्र", AnusuchiId = 1 },
                    new()  {  SerialNumber = 8, Name = "प्रदेशको वातावरण संरक्षण ऐन, २०७७ बमोजिम संक्षिप्त वातावरणीय परीक्षण (BES) वा प्रारम्भिक वातावरणीय परीक्षण (IEE) वा वातावरणीय प्रभाव मूल्याङ्कन (EIA) को प्रतिवेदन स्वीकृत भएको", AnusuchiId = 1 },
                    new()  {  SerialNumber = 9, Name = "स्वास्थ्य संस्था रहने स्थानको उपयुक्तता भएको", AnusuchiId = 1 },
                    new()  {   SerialNumber = 10, Name = "स्वास्थ्य संस्थाले सेवा पुर्याउने क्षेत्रको बृहतता र जनसंख्याको घनत्व घना/बाक्लो भएको", AnusuchiId = 1 },
                    new()  {   SerialNumber = 11, Name = "स्वास्थ्य संस्था वा अस्पताल स्थापना हुने स्थानमा सडक यातायातको पहुँच भएको", AnusuchiId = 1 },
                    new()  {   SerialNumber = 12, Name = "स्वास्थ्य संस्था वा अस्पतालको कुल सञ्‍चालक समितिको सदस्य मध्ये कम्तिमा दुई तिहाई सदस्य नेपाली नागरिक भएको", AnusuchiId = 1 },
                    new()  {   SerialNumber = 13, Name = "सम्बन्धित स्थानीय तहको सिफारिस पत्र", AnusuchiId = 1 },
                    new()  {   SerialNumber = 14, Name = "सम्बन्धित जनस्वास्थ्य कार्यालयको सिफारिस पत्र", AnusuchiId = 1 },

                    new()  {  SerialNumber = 1, Name = "स्वास्थ्य संस्थाको व्यवसाय दर्ता भएको प्रमाणपत्रको प्रतिलिपि", AnusuchiId = 2 },
                    new()  {  SerialNumber = 2, Name = "स्वास्थ्य संस्थाको प्रबन्धपत्र र नियमावलीको प्रतिलिपि", AnusuchiId = 2 },
                    new()  {  SerialNumber = 3, Name = "स्वास्थ्य संस्थाको स्थायी लेखा नम्बर प्रमाणपत्र", AnusuchiId = 2 },
                    new()  {  SerialNumber = 4, Name = "स्वास्थ्य संस्थाको नवीकरण भएको", AnusuchiId = 2 },
                    new()  {  SerialNumber = 5, Name = "स्वास्थ्य सेवा सञ्‍चालन अनुमति पत्र", AnusuchiId = 2 },
                    new()  {  SerialNumber = 6, Name = "हालको स्वास्थ्य सेवा सञ्‍चालनको नवीकरण भएको", AnusuchiId = 2 },
                    new()  {  SerialNumber = 7, Name = "स्वास्थ्य संस्थाको कर चुक्ता (अघिल्लो आ.व. सम्मको) प्रमाणपत्र वा कर विवरण बुझाएको रसिद (नयाँको हकमा आवश्यक नहुने)", AnusuchiId = 2 },
                    new()  {  SerialNumber = 8, Name = "स्वास्थ्य संस्थाको गुरु योजना (Master Plan) र कार्य योजना (Business Plan)", AnusuchiId = 2 },
                    new()  {  SerialNumber = 9, Name = "प्रदेश स्वास्थ्य सेवा ऐन, २०७५ तथा प्रदेश स्वास्थ्य सेवा नियमावली, २०७६ बमोजिम जग्गाको मापदण्ड पुरा भएको", AnusuchiId = 2 },
                    new()  {   SerialNumber = 10, Name = "स्वास्थ्य संस्था रहने भवनको नक्सा र नक्सा पास प्रमाणपत्र तथा जग्गा वा घर भाडा/लिजको हकमा सम्झौता पत्र", AnusuchiId = 2 },
                    new()  {   SerialNumber = 11, Name = "प्रदेशको वातावरण संरक्षण ऐन, २०७७ बमोजिम संक्षिप्त वातावरणीय परीक्षण (BES) वा प्रारम्भिक वातावरणीय परीक्षण (IEE) वा वातावरणीय प्रभाव मूल्याङ्कन (EIA) को प्रतिवेदन स्वीकृत भएको", AnusuchiId = 2 },
                    new()  {   SerialNumber = 12, Name = "स्वास्थ्य संस्थाको आयकर प्रमाणपत्र वेबसाइट वा सबैले देख्‍ने ठाउँमा राखेको", AnusuchiId = 2 },
                    new()  {   SerialNumber = 13, Name = "स्वास्थ्य संस्थाको नागरिक बडापत्र (सेवा, प्रक्रिया, सुविधा र शुल्क सहितको विवरण) वेबसाइट र सबैले देख्‍ने ठाउँमा प्रदर्शनमा राखेको", AnusuchiId = 2 },
                    new()  {   SerialNumber = 14, Name = "स्वास्थ्य संस्थाले प्रचलित कानूनमा तोकिए बमोजिम स्वास्थ्य औजार, मेशिन र उपकरण खरिद गरेको", AnusuchiId = 2 },
                    new()  {   SerialNumber = 15, Name = "सरकारले तोकिएको ढाँचामा (DHIS/AHMIS अनुसार) मासिक रुपमा प्रतिवेदन प्रविष्ट गर्ने वा जनस्वास्थ्य कार्यालयमा बुझाउने गरेको", AnusuchiId = 2 },
                    new()  {   SerialNumber = 16, Name = "जनस्वास्थ्य कार्यालय र सम्बन्धित स्थानीय तहको सिफारिस पत्र", AnusuchiId = 2 },
                    new()  {   SerialNumber = 17, Name = "गत आ.व. सम्मको लेखापरीक्षण प्रतिवेदन", AnusuchiId = 2 },
                    new()  {   SerialNumber = 18, Name = "स्वमूल्याङ्कन फाराम भरी प्रतिवेदन पेश गरेको र दस्तुर तिरेको रसिद/निस्सा पेश गरिएको", AnusuchiId = 2 },
                    new()  {   SerialNumber = 19, Name = "अस्पताल स्थापना हुने स्थानमा सडक यातायातको पहुँच भएको", AnusuchiId = 2 },
                    new()  {   SerialNumber = 20, Name = "बेवारिसे, अति गरिब, विपन्‍न, अपाङ्गता भएका विपन्‍न र असहाय बिरामीका लागि कुल शय्याको १०% शय्या छुट्याई उपलब्ध सेवा, औषधी तथा उपचार खर्च व्यहोर्ने गरेको", AnusuchiId = 2 },
                    new()  {   SerialNumber = 21, Name = "स्वास्थ्य संस्था वा अस्पतालको कूल सञ्‍चालक समितिको सदस्य मध्ये कम्तिमा दुई तिहाई सदस्य नेपाली नागरिक भएको", AnusuchiId = 2 },

                    new()  { SerialNumber = 1, Name = "स्वास्थ्य संस्था दर्ता भएको प्रमाणपत्र", AnusuchiId = 3 },
                    new()  { SerialNumber = 2, Name = "स्वास्थ्य संस्थाको स्थायी लेखा नम्बर प्रमाणपत्र", AnusuchiId = 3 },
                    new()  { SerialNumber = 3, Name = "स्थायी लेखा/आयकर प्रमाणपत्र सबैले देख्‍ने ठाउँमा राखिएको", AnusuchiId = 3 },
                    new()  { SerialNumber = 4, Name = "स्वास्थ्य संस्थाको प्रबन्धपत्र/विधान/नियमावलीको प्रतिलिपि", AnusuchiId = 3 },
                    new()  { SerialNumber = 5, Name = "स्वास्थ्य संस्थाको नवीकरण भएको (नयाँको हकमा आवश्यक नपर्ने)", AnusuchiId = 3 },
                    new()  { SerialNumber = 6, Name = "स्वास्थ्य सेवा सञ्‍चालन अनुमति पत्र (नयाँ सञ्‍चालन अनुमतिको हकमा आशय पत्रको प्रमाणपत्र)", AnusuchiId = 3 },
                    new()  { SerialNumber = 7, Name = "अघिल्लो आ.व. सम्मको स्वास्थ्य संस्थाको कर चुक्ता प्रमाणपत्र (नयाँको हकमा आवश्यक नपर्ने)", AnusuchiId = 3 },
                    new()  { SerialNumber = 8, Name = "स्वास्थ्य संस्थाको नागरिक बडापत्र (सेवा, प्रक्रिया, सुविधा र शुल्क सहितको विवरण) वेबसाइट र सबैले देख्‍ने ठाउँमा प्रदर्शनमा राखेको", AnusuchiId = 3 },
                    new()  { SerialNumber = 9, Name = "प्रदेश वातावरण संरक्षण ऐन २०७७ बमोजिम संक्षिप्त वातावरणीय परीक्षण (BES) वा प्रारम्भिक वातावरणीय परीक्षण (IEE) वा वातावरणीय प्रभाव मूल्याङ्कन (EIA) को स्वीकृति पत्र", AnusuchiId = 3 },
                    new()  { SerialNumber = 10, Name = "सरकारले तोकिएको ढाँचामा (HMIS /AHMIS अनुसार) मासिक रुपमा प्रतिवेदन प्रविष्ट गर्ने/बुझाउने गरेको", AnusuchiId = 3 },
                    new()  { SerialNumber = 11, Name = "(नयाँको हकमा आवश्यक नपर्ने)", AnusuchiId = 3 },
                    new()  { SerialNumber = 12, Name = "स्वास्थ्य संस्था भवनको निर्माण सम्पन्‍न प्रमाणपत्र", AnusuchiId = 3 },
                    new()  { SerialNumber = 13, Name = "(आशयपत्रको हकमा आवश्यक नपर्ने)", AnusuchiId = 3 },
                    new()  { SerialNumber = 14, Name = "गत आ.व. को वार्षिक लेखा परीक्षण प्रतिवेदन (नयाँको हकमा आवश्यक नपर्ने)", AnusuchiId = 3 },
                    new()  { SerialNumber = 15, Name = "स्वास्थ्य संस्थाको कार्य योजना (Business Plan)", AnusuchiId = 3 },
                    new()  { SerialNumber = 16, Name = "स्वास्थ्य संस्थाको आफ्नै वेवसाइट (अद्यावधिक गरिएको) भएको", AnusuchiId = 3 },
                    new()  { SerialNumber = 17, Name = "सम्बन्धित स्थानीय तहको सिफारिस पत्र", AnusuchiId = 3 },
                    new()  { SerialNumber = 18, Name = "सम्बन्धित जनस्वास्थ्य कार्यालयको सिफारिस पत्र", AnusuchiId = 3 },
                    new()  { SerialNumber = 19, Name = "बेवारिसे, अति गरिब, विपन्‍न, अपाङ्गता भएका विपन्‍न र असहाय बिरामीका लागि कूल शय्याको १०% शय्या छुट्याई उपलब्ध सेवा, औषधी तथा उपचार खर्च व्यहोर्ने गरेको प्रमाण/निस्सा", AnusuchiId = 3 },
                    new()  { SerialNumber = 20, Name = "(नयाँको हकमा आवश्यक नपर्ने)", AnusuchiId = 3 },
                    new()  { SerialNumber = 21, Name = "स्वास्थ्य संस्था वा अस्पतालको कूल सञ्‍चालक समितिको सदस्य मध्ये कम्तिमा दुई तिहाई सदस्य नेपाली नागरिक भएको", AnusuchiId = 3 }

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

            var anusuchi = await context.Set<Anusuchi>().FirstOrDefaultAsync(cancellationToken);
            if (anusuchi is null)
            {
                List<Anusuchi> anusuchis = [
                    new() { RelatedToDafaNo = "दफा ३ सँग सÌबिÆधत", AnusuchiName = "नयाँ ÖवाÖÃय सÖं थाको सेवा स¼ चालन पूवाªधार Öवीकृितको आशय पý (लेटर अफ इÆटेÆट) ÿदान गनªका लािग वÖतगुत मापदÁड" },
                    new() { RelatedToDafaNo = "दफा ३ सँग सÌबिÆधत", AnusuchiName = "ÖवाÖÃय सÖं थाको ÖतरोÆ नितका लािग पवूाªधार Öवीकृितको आशय पý (लेटर अफ इÆटेÆट) ÿदान गनªका लािग\r\nवÖतुगत मापदÁड" },
                    new() { RelatedToDafaNo = "दफा ४ सँग सÌबिÆधत", AnusuchiName = "ÖवाÖÃय सÖं थाको वैधािनक ÓयवÖथा सÌबÆधी मापदÁड" },
                ];

                await context.Set<Anusuchi>().AddRangeAsync(anusuchis, cancellationToken);
                await context.SaveChangesAsync(cancellationToken);
            }


            var mapdanda = context.Set<Mapdanda>().FirstOrDefault();
            if (mapdanda is null)
            {
                List<Mapdanda> mapdandas = [
                    new()  {  SerialNumber = 1, Name = "स्वास्थ्य संस्थाको व्यवसाय दर्ता भएको प्रमाणपत्रको प्रतिलिपि", AnusuchiId = 1 },
                    new()  {  SerialNumber = 2, Name = "स्वास्थ्य संस्थाको प्रबन्धपत्र र नियमावलीको प्रतिलिपि", AnusuchiId = 1 },
                    new()  {  SerialNumber = 3, Name = "स्वास्थ्य संस्थाको स्थायी लेखा नम्बर प्रमाणपत्र", AnusuchiId = 1 },
                    new()  {  SerialNumber = 4, Name = "स्वास्थ्य संस्थाको कर चुक्ता (अघिल्लो आ.व. सम्मको) प्रमाणपत्र वा कर विवरण बुझाएको रसिद (नयाँको हकमा आवश्यक नहुने)", AnusuchiId = 1 },
                    new()  {  SerialNumber = 5, Name = "स्वास्थ्य संस्थाको गुरु योजना (Master Plan ) र कार्य योजना (Business Plan)", AnusuchiId = 1 },
                    new()  {  SerialNumber = 6, Name = "प्रदेश स्वास्थ्य सेवा ऐन, २०७५ तथा प्रदेश स्वास्थ्य सेवा नियमावली, २०७६ बमोजिम जग्गाको मापदण्ड पुरा भएको", AnusuchiId = 1 },
                    new()  {  SerialNumber = 7, Name = "स्वास्थ्य संस्था रहने भवनको नक्सा र नक्सा पास प्रमाणपत्र तथा जग्गा वा घर भाडा/लिजको हकमा वडा/पालिकाको रोहवरमा भएको सम्झौता पत्र", AnusuchiId = 1 },
                    new()  {  SerialNumber = 8, Name = "प्रदेशको वातावरण संरक्षण ऐन, २०७७ बमोजिम संक्षिप्त वातावरणीय परीक्षण (BES) वा प्रारम्भिक वातावरणीय परीक्षण (IEE) वा वातावरणीय प्रभाव मूल्याङ्कन (EIA) को प्रतिवेदन स्वीकृत भएको", AnusuchiId = 1 },
                    new()  {  SerialNumber = 9, Name = "स्वास्थ्य संस्था रहने स्थानको उपयुक्तता भएको", AnusuchiId = 1 },
                    new()  {   SerialNumber = 10, Name = "स्वास्थ्य संस्थाले सेवा पुर्याउने क्षेत्रको बृहतता र जनसंख्याको घनत्व घना/बाक्लो भएको", AnusuchiId = 1 },
                    new()  {   SerialNumber = 11, Name = "स्वास्थ्य संस्था वा अस्पताल स्थापना हुने स्थानमा सडक यातायातको पहुँच भएको", AnusuchiId = 1 },
                    new()  {   SerialNumber = 12, Name = "स्वास्थ्य संस्था वा अस्पतालको कुल सञ्‍चालक समितिको सदस्य मध्ये कम्तिमा दुई तिहाई सदस्य नेपाली नागरिक भएको", AnusuchiId = 1 },
                    new()  {   SerialNumber = 13, Name = "सम्बन्धित स्थानीय तहको सिफारिस पत्र", AnusuchiId = 1 },
                    new()  {   SerialNumber = 14, Name = "सम्बन्धित जनस्वास्थ्य कार्यालयको सिफारिस पत्र", AnusuchiId = 1 },

                    new()  {  SerialNumber = 1, Name = "स्वास्थ्य संस्थाको व्यवसाय दर्ता भएको प्रमाणपत्रको प्रतिलिपि", AnusuchiId = 2 },
                    new()  {  SerialNumber = 2, Name = "स्वास्थ्य संस्थाको प्रबन्धपत्र र नियमावलीको प्रतिलिपि", AnusuchiId = 2 },
                    new()  {  SerialNumber = 3, Name = "स्वास्थ्य संस्थाको स्थायी लेखा नम्बर प्रमाणपत्र", AnusuchiId = 2 },
                    new()  {  SerialNumber = 4, Name = "स्वास्थ्य संस्थाको नवीकरण भएको", AnusuchiId = 2 },
                    new()  {  SerialNumber = 5, Name = "स्वास्थ्य सेवा सञ्‍चालन अनुमति पत्र", AnusuchiId = 2 },
                    new()  {  SerialNumber = 6, Name = "हालको स्वास्थ्य सेवा सञ्‍चालनको नवीकरण भएको", AnusuchiId = 2 },
                    new()  {  SerialNumber = 7, Name = "स्वास्थ्य संस्थाको कर चुक्ता (अघिल्लो आ.व. सम्मको) प्रमाणपत्र वा कर विवरण बुझाएको रसिद (नयाँको हकमा आवश्यक नहुने)", AnusuchiId = 2 },
                    new()  {  SerialNumber = 8, Name = "स्वास्थ्य संस्थाको गुरु योजना (Master Plan) र कार्य योजना (Business Plan)", AnusuchiId = 2 },
                    new()  {  SerialNumber = 9, Name = "प्रदेश स्वास्थ्य सेवा ऐन, २०७५ तथा प्रदेश स्वास्थ्य सेवा नियमावली, २०७६ बमोजिम जग्गाको मापदण्ड पुरा भएको", AnusuchiId = 2 },
                    new()  {   SerialNumber = 10, Name = "स्वास्थ्य संस्था रहने भवनको नक्सा र नक्सा पास प्रमाणपत्र तथा जग्गा वा घर भाडा/लिजको हकमा सम्झौता पत्र", AnusuchiId = 2 },
                    new()  {   SerialNumber = 11, Name = "प्रदेशको वातावरण संरक्षण ऐन, २०७७ बमोजिम संक्षिप्त वातावरणीय परीक्षण (BES) वा प्रारम्भिक वातावरणीय परीक्षण (IEE) वा वातावरणीय प्रभाव मूल्याङ्कन (EIA) को प्रतिवेदन स्वीकृत भएको", AnusuchiId = 2 },
                    new()  {   SerialNumber = 12, Name = "स्वास्थ्य संस्थाको आयकर प्रमाणपत्र वेबसाइट वा सबैले देख्‍ने ठाउँमा राखेको", AnusuchiId = 2 },
                    new()  {   SerialNumber = 13, Name = "स्वास्थ्य संस्थाको नागरिक बडापत्र (सेवा, प्रक्रिया, सुविधा र शुल्क सहितको विवरण) वेबसाइट र सबैले देख्‍ने ठाउँमा प्रदर्शनमा राखेको", AnusuchiId = 2 },
                    new()  {   SerialNumber = 14, Name = "स्वास्थ्य संस्थाले प्रचलित कानूनमा तोकिए बमोजिम स्वास्थ्य औजार, मेशिन र उपकरण खरिद गरेको", AnusuchiId = 2 },
                    new()  {   SerialNumber = 15, Name = "सरकारले तोकिएको ढाँचामा (DHIS/AHMIS अनुसार) मासिक रुपमा प्रतिवेदन प्रविष्ट गर्ने वा जनस्वास्थ्य कार्यालयमा बुझाउने गरेको", AnusuchiId = 2 },
                    new()  {   SerialNumber = 16, Name = "जनस्वास्थ्य कार्यालय र सम्बन्धित स्थानीय तहको सिफारिस पत्र", AnusuchiId = 2 },
                    new()  {   SerialNumber = 17, Name = "गत आ.व. सम्मको लेखापरीक्षण प्रतिवेदन", AnusuchiId = 2 },
                    new()  {   SerialNumber = 18, Name = "स्वमूल्याङ्कन फाराम भरी प्रतिवेदन पेश गरेको र दस्तुर तिरेको रसिद/निस्सा पेश गरिएको", AnusuchiId = 2 },
                    new()  {   SerialNumber = 19, Name = "अस्पताल स्थापना हुने स्थानमा सडक यातायातको पहुँच भएको", AnusuchiId = 2 },
                    new()  {   SerialNumber = 20, Name = "बेवारिसे, अति गरिब, विपन्‍न, अपाङ्गता भएका विपन्‍न र असहाय बिरामीका लागि कुल शय्याको १०% शय्या छुट्याई उपलब्ध सेवा, औषधी तथा उपचार खर्च व्यहोर्ने गरेको", AnusuchiId = 2 },
                    new()  {   SerialNumber = 21, Name = "स्वास्थ्य संस्था वा अस्पतालको कूल सञ्‍चालक समितिको सदस्य मध्ये कम्तिमा दुई तिहाई सदस्य नेपाली नागरिक भएको", AnusuchiId = 2 },

                    new()  { SerialNumber = 1, Name = "स्वास्थ्य संस्था दर्ता भएको प्रमाणपत्र", AnusuchiId = 3 },
                    new()  { SerialNumber = 2, Name = "स्वास्थ्य संस्थाको स्थायी लेखा नम्बर प्रमाणपत्र", AnusuchiId = 3 },
                    new()  { SerialNumber = 3, Name = "स्थायी लेखा/आयकर प्रमाणपत्र सबैले देख्‍ने ठाउँमा राखिएको", AnusuchiId = 3 },
                    new()  { SerialNumber = 4, Name = "स्वास्थ्य संस्थाको प्रबन्धपत्र/विधान/नियमावलीको प्रतिलिपि", AnusuchiId = 3 },
                    new()  { SerialNumber = 5, Name = "स्वास्थ्य संस्थाको नवीकरण भएको (नयाँको हकमा आवश्यक नपर्ने)", AnusuchiId = 3 },
                    new()  { SerialNumber = 6, Name = "स्वास्थ्य सेवा सञ्‍चालन अनुमति पत्र (नयाँ सञ्‍चालन अनुमतिको हकमा आशय पत्रको प्रमाणपत्र)", AnusuchiId = 3 },
                    new()  { SerialNumber = 7, Name = "अघिल्लो आ.व. सम्मको स्वास्थ्य संस्थाको कर चुक्ता प्रमाणपत्र (नयाँको हकमा आवश्यक नपर्ने)", AnusuchiId = 3 },
                    new()  { SerialNumber = 8, Name = "स्वास्थ्य संस्थाको नागरिक बडापत्र (सेवा, प्रक्रिया, सुविधा र शुल्क सहितको विवरण) वेबसाइट र सबैले देख्‍ने ठाउँमा प्रदर्शनमा राखेको", AnusuchiId = 3 },
                    new()  { SerialNumber = 9, Name = "प्रदेश वातावरण संरक्षण ऐन २०७७ बमोजिम संक्षिप्त वातावरणीय परीक्षण (BES) वा प्रारम्भिक वातावरणीय परीक्षण (IEE) वा वातावरणीय प्रभाव मूल्याङ्कन (EIA) को स्वीकृति पत्र", AnusuchiId = 3 },
                    new()  { SerialNumber = 10, Name = "सरकारले तोकिएको ढाँचामा (HMIS /AHMIS अनुसार) मासिक रुपमा प्रतिवेदन प्रविष्ट गर्ने/बुझाउने गरेको", AnusuchiId = 3 },
                    new()  { SerialNumber = 11, Name = "(नयाँको हकमा आवश्यक नपर्ने)", AnusuchiId = 3 },
                    new()  { SerialNumber = 12, Name = "स्वास्थ्य संस्था भवनको निर्माण सम्पन्‍न प्रमाणपत्र", AnusuchiId = 3 },
                    new()  { SerialNumber = 13, Name = "(आशयपत्रको हकमा आवश्यक नपर्ने)", AnusuchiId = 3 },
                    new()  { SerialNumber = 14, Name = "गत आ.व. को वार्षिक लेखा परीक्षण प्रतिवेदन (नयाँको हकमा आवश्यक नपर्ने)", AnusuchiId = 3 },
                    new()  { SerialNumber = 15, Name = "स्वास्थ्य संस्थाको कार्य योजना (Business Plan)", AnusuchiId = 3 },
                    new()  { SerialNumber = 16, Name = "स्वास्थ्य संस्थाको आफ्नै वेवसाइट (अद्यावधिक गरिएको) भएको", AnusuchiId = 3 },
                    new()  { SerialNumber = 17, Name = "सम्बन्धित स्थानीय तहको सिफारिस पत्र", AnusuchiId = 3 },
                    new()  { SerialNumber = 18, Name = "सम्बन्धित जनस्वास्थ्य कार्यालयको सिफारिस पत्र", AnusuchiId = 3 },
                    new()  { SerialNumber = 19, Name = "बेवारिसे, अति गरिब, विपन्‍न, अपाङ्गता भएका विपन्‍न र असहाय बिरामीका लागि कूल शय्याको १०% शय्या छुट्याई उपलब्ध सेवा, औषधी तथा उपचार खर्च व्यहोर्ने गरेको प्रमाण/निस्सा", AnusuchiId = 3 },
                    new()  { SerialNumber = 20, Name = "(नयाँको हकमा आवश्यक नपर्ने)", AnusuchiId = 3 },
                    new()  { SerialNumber = 21, Name = "स्वास्थ्य संस्था वा अस्पतालको कूल सञ्‍चालक समितिको सदस्य मध्ये कम्तिमा दुई तिहाई सदस्य नेपाली नागरिक भएको", AnusuchiId = 3 }

                ];
                await context.Set<Mapdanda>().AddRangeAsync(mapdandas, cancellationToken);
                await context.SaveChangesAsync(cancellationToken);
            }
        });
}

