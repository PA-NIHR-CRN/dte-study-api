# Database Migrations with Step-Based SQL Support

This project uses Entity Framework Core for migrations, with enhanced support for applying custom SQL scripts in ordered steps, followed by a common step file via the `MigrationBuilderExtensions`.

* Steps are **0-indexed** (`.0.up.sql`, `.1.up.sql`, etc.)
* Common SQL files (`.up.sql`, `.down.sql`) should be used for final inserts or transformations

---

## Folder Structure

SQL migration scripts are placed in:

```
Migrations/Scripts/{MigrationName}/
```

Where `{MigrationName}` matches the EF migration filename (e.g. `20240519_MyMigration`).

```
Migrations/Scripts/20240519_MyMigration/
├── 20240519_MyMigration.0.up.sql
├── 20240519_MyMigration.1.up.sql
├── 20240519_MyMigration.up.sql
├── 20240519_MyMigration.0.down.sql
├── 20240519_MyMigration.down.sql
```

---

## Supported SQL Files

| Type              | File Pattern                  | Purpose                           |
| ----------------- | ----------------------------- | --------------------------------- |
| Step SQL          | `{Name}.{StepIndex}.up.sql`   | SQL to run at a specific point    |
| Common SQL        | `{Name}.up.sql`               | SQL to run at the **end** of Up() |
| Step SQL (Down)   | `{Name}.{StepIndex}.down.sql` | Rollback step                     |
| Common SQL (Down) | `{Name}.down.sql`             | Final cleanup in Down()           |

All files are resolved case-insensitively and executed in order by step suffix.

---

## MigrationBuilder Extension Methods

### `RunSqlStep(...)`

Runs a specific step script by index.

```csharp
migrationBuilder.RunSqlStep("20240519_MyMigration", MigrationDirection.Up, 0);
```

### `RunCommonSql(...)`

Runs the common `*.up.sql` or `*.down.sql` file, if present.

```csharp
migrationBuilder.RunCommonSql("20240519_MyMigration", MigrationDirection.Up);
```

### `CheckForDuplicateSteps(...)`

Ensures no conflicting step identifiers (e.g. duplicate `.1.up.sql`) exist before proceeding.

```csharp
MigrationBuilderExtensions.CheckForDuplicateSteps("20240519_MyMigration", MigrationDirection.Up);
```

---

## Example Migration

```csharp
protected override void Up(MigrationBuilder migrationBuilder)
{
    const string Script = "20240519_MyMigration";

    MigrationBuilderExtensions.CheckForDuplicateSteps(Script, MigrationDirection.Up);

    migrationBuilder.RunSqlStep(Script, MigrationDirection.Up, 0);

    migrationBuilder.CreateIndex(...);

    migrationBuilder.RunSqlStep(Script, MigrationDirection.Up, 1);

    migrationBuilder.RunCommonSql(Script, MigrationDirection.Up);
}
```