namespace OyunMVC.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Olustur : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Yorum",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        HaberID = c.Int(nullable: false),
                        KullaniciID = c.Int(nullable: false),
                        GelenYorum = c.String(),
                        EklenmeTarihi = c.DateTime(nullable: false),
                        AktifMi = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Haber", t => t.HaberID, cascadeDelete: true)
                .ForeignKey("dbo.Kullanici", t => t.KullaniciID, cascadeDelete: false)
                .Index(t => t.HaberID)
                .Index(t => t.KullaniciID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Yorum", "KullaniciID", "dbo.Kullanici");
            DropForeignKey("dbo.Yorum", "HaberID", "dbo.Haber");
            DropIndex("dbo.Yorum", new[] { "KullaniciID" });
            DropIndex("dbo.Yorum", new[] { "HaberID" });
            DropTable("dbo.Yorum");
        }
    }
}
