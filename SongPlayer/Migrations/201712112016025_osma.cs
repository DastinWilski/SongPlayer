namespace SongPlayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class osma : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Playlists", name: "User_Id", newName: "ApplicationUserId");
            RenameIndex(table: "dbo.Playlists", name: "IX_User_Id", newName: "IX_ApplicationUserId");
            DropColumn("dbo.Playlists", "UserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Playlists", "UserId", c => c.Int(nullable: false));
            RenameIndex(table: "dbo.Playlists", name: "IX_ApplicationUserId", newName: "IX_User_Id");
            RenameColumn(table: "dbo.Playlists", name: "ApplicationUserId", newName: "User_Id");
        }
    }
}
