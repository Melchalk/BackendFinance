using FluentMigrator;

namespace BackendFinance.Dal.Migrations;

[Migration(20251206143100, TransactionBehavior.None)]
public class InitSchema : Migration
{
    public override void Up()
    {
        Create.Table("incomes")
            .WithColumn("id").AsInt32().PrimaryKey("incomes_pk").Identity()
            .WithColumn("amount").AsInt32().NotNullable()
            .WithColumn("created_at").AsDateTimeOffset().NotNullable();

        Create.Table("expenses")
            .WithColumn("id").AsInt32().PrimaryKey("expenses_pk").Identity()
            .WithColumn("amount").AsInt32().NotNullable()
            .WithColumn("created_at").AsDateTimeOffset().NotNullable();
    }

    public override void Down()
    {
        Delete.Table("incomes");
        Delete.Table("expenses");
    }
}