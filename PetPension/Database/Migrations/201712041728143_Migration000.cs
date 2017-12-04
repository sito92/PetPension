namespace Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration000 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Pets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RoomTypeId = c.Int(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Reservations",
                c => new
                    {
                        ReservationTimeId = c.Int(nullable: false),
                        Id = c.Int(nullable: false),
                        CreatedTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ReservationTimeId)
                .ForeignKey("dbo.Pets", t => t.Id, cascadeDelete: true)
                .ForeignKey("dbo.ReservationTimes", t => t.ReservationTimeId)
                .Index(t => t.ReservationTimeId)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.ReservationTimes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        From = c.DateTime(nullable: false),
                        To = c.DateTime(nullable: false),
                        Room_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Rooms", t => t.Room_Id)
                .Index(t => t.Room_Id);
            
            CreateTable(
                "dbo.Rooms",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RoomTypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ReservationTimes", "Room_Id", "dbo.Rooms");
            DropForeignKey("dbo.Reservations", "ReservationTimeId", "dbo.ReservationTimes");
            DropForeignKey("dbo.Reservations", "Id", "dbo.Pets");
            DropIndex("dbo.ReservationTimes", new[] { "Room_Id" });
            DropIndex("dbo.Reservations", new[] { "Id" });
            DropIndex("dbo.Reservations", new[] { "ReservationTimeId" });
            DropTable("dbo.Rooms");
            DropTable("dbo.ReservationTimes");
            DropTable("dbo.Reservations");
            DropTable("dbo.Pets");
        }
    }
}
