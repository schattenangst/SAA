namespace ApiAuthorizationAA.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitializeDB : DbMigration
    {
        public override void Up()
        {
            MoveTable(name: "secure.siara_cat_usuarios_web", newSchema: "dbo");
            CreateTable(
                "dbo.UsuarioTest",
                c => new
                    {
                        IdUsuariosTest = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 50),
                        Activo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.IdUsuariosTest);
            
            AddColumn("secure.SiaraHistoricoHash", "id_usuario", c => c.String(nullable: false));
            AddColumn("secure.SiaraUsuarioWebHash", "id_usuario", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.siara_cat_usuarios_web", "SiaraHistoricHash_IdSiaraHistoricHash", c => c.Int());
            CreateIndex("dbo.siara_cat_usuarios_web", "SiaraHistoricHash_IdSiaraHistoricHash");
            CreateIndex("secure.SiaraUsuarioWebHash", "id_usuario");
            AddForeignKey("dbo.siara_cat_usuarios_web", "SiaraHistoricHash_IdSiaraHistoricHash", "secure.SiaraHistoricoHash", "IdSiaraHistoricoHash");
            AddForeignKey("secure.SiaraUsuarioWebHash", "id_usuario", "dbo.siara_cat_usuarios_web", "id_usuario", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("secure.SiaraUsuarioWebHash", "id_usuario", "dbo.siara_cat_usuarios_web");
            DropForeignKey("dbo.siara_cat_usuarios_web", "SiaraHistoricHash_IdSiaraHistoricHash", "secure.SiaraHistoricoHash");
            DropIndex("secure.SiaraUsuarioWebHash", new[] { "id_usuario" });
            DropIndex("dbo.siara_cat_usuarios_web", new[] { "SiaraHistoricHash_IdSiaraHistoricHash" });
            DropColumn("dbo.siara_cat_usuarios_web", "SiaraHistoricHash_IdSiaraHistoricHash");
            DropColumn("secure.SiaraUsuarioWebHash", "id_usuario");
            DropColumn("secure.SiaraHistoricoHash", "id_usuario");
            DropTable("dbo.UsuarioTest");
            MoveTable(name: "dbo.siara_cat_usuarios_web", newSchema: "secure");
        }
    }
}
