namespace ApiAuthorizationAA.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ForeignKeys : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("secure.ControlCifrado", "SiaraHistoricHash_IdSiaraHistoricHash", "secure.SiaraHistoricoHash");
            DropForeignKey("dbo.siara_cat_usuarios_web", "SiaraHistoricHash_IdSiaraHistoricHash", "secure.SiaraHistoricoHash");
            DropIndex("secure.ControlCifrado", new[] { "SiaraHistoricHash_IdSiaraHistoricHash" });
            DropIndex("dbo.siara_cat_usuarios_web", new[] { "SiaraHistoricHash_IdSiaraHistoricHash" });
            AlterColumn("secure.SiaraHistoricoHash", "id_usuario", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("secure.SiaraHistoricoHash", "id_usuario");
            CreateIndex("secure.SiaraHistoricoHash", "IdControlCifrado");
            AddForeignKey("secure.SiaraHistoricoHash", "IdControlCifrado", "secure.ControlCifrado", "IdControlCifrado", cascadeDelete: true);
            AddForeignKey("secure.SiaraHistoricoHash", "id_usuario", "dbo.siara_cat_usuarios_web", "id_usuario", cascadeDelete: true);
            DropColumn("secure.ControlCifrado", "SiaraHistoricHash_IdSiaraHistoricHash");
            DropColumn("dbo.siara_cat_usuarios_web", "SiaraHistoricHash_IdSiaraHistoricHash");
            DropColumn("secure.SiaraUsuarioWebHash", "IdUsuarioWebHash");
        }
        
        public override void Down()
        {
            AddColumn("secure.SiaraUsuarioWebHash", "IdUsuarioWebHash", c => c.String(nullable: false, maxLength: 20));
            AddColumn("dbo.siara_cat_usuarios_web", "SiaraHistoricHash_IdSiaraHistoricHash", c => c.Int());
            AddColumn("secure.ControlCifrado", "SiaraHistoricHash_IdSiaraHistoricHash", c => c.Int());
            DropForeignKey("secure.SiaraHistoricoHash", "id_usuario", "dbo.siara_cat_usuarios_web");
            DropForeignKey("secure.SiaraHistoricoHash", "IdControlCifrado", "secure.ControlCifrado");
            DropIndex("secure.SiaraHistoricoHash", new[] { "IdControlCifrado" });
            DropIndex("secure.SiaraHistoricoHash", new[] { "id_usuario" });
            AlterColumn("secure.SiaraHistoricoHash", "id_usuario", c => c.String(nullable: false));
            CreateIndex("dbo.siara_cat_usuarios_web", "SiaraHistoricHash_IdSiaraHistoricHash");
            CreateIndex("secure.ControlCifrado", "SiaraHistoricHash_IdSiaraHistoricHash");
            AddForeignKey("dbo.siara_cat_usuarios_web", "SiaraHistoricHash_IdSiaraHistoricHash", "secure.SiaraHistoricoHash", "IdSiaraHistoricoHash");
            AddForeignKey("secure.ControlCifrado", "SiaraHistoricHash_IdSiaraHistoricHash", "secure.SiaraHistoricoHash", "IdSiaraHistoricoHash");
        }
    }
}
