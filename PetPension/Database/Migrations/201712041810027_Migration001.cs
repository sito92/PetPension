namespace Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration001 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ReservationTimes", "Room_Id", "dbo.Rooms");
            DropIndex("dbo.ReservationTimes", new[] { "Room_Id" });
            AddForeignKey("dbo.Reservations", "Id", "dbo.Rooms", "Id", cascadeDelete: true);
            DropColumn("dbo.ReservationTimes", "Room_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ReservationTimes", "Room_Id", c => c.Int());
            DropForeignKey("dbo.Reservations", "Id", "dbo.Rooms");
            CreateIndex("dbo.ReservationTimes", "Room_Id");
            AddForeignKey("dbo.ReservationTimes", "Room_Id", "dbo.Rooms", "Id");
        }
    }
}
