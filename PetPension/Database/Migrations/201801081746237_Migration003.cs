namespace Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration003 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Reservations", "Id", "dbo.Pets");
            DropIndex("dbo.Reservations", new[] { "Id" });
            AddColumn("dbo.Reservations", "PetId", c => c.Int());
            CreateIndex("dbo.Reservations", "PetId");
            AddForeignKey("dbo.Reservations", "PetId", "dbo.Pets", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reservations", "PetId", "dbo.Pets");
            DropIndex("dbo.Reservations", new[] { "PetId" });
            DropColumn("dbo.Reservations", "PetId");
            CreateIndex("dbo.Reservations", "Id");
            AddForeignKey("dbo.Reservations", "Id", "dbo.Pets", "Id", cascadeDelete: true);
        }
    }
}
