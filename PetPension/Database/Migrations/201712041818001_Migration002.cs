namespace Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration002 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Reservations", "Id", "dbo.Rooms");
            AddColumn("dbo.Reservations", "RoomId", c => c.Int(nullable: false));
            CreateIndex("dbo.Reservations", "RoomId");
            AddForeignKey("dbo.Reservations", "RoomId", "dbo.Rooms", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reservations", "RoomId", "dbo.Rooms");
            DropIndex("dbo.Reservations", new[] { "RoomId" });
            DropColumn("dbo.Reservations", "RoomId");
            AddForeignKey("dbo.Reservations", "Id", "dbo.Rooms", "Id", cascadeDelete: true);
        }
    }
}
