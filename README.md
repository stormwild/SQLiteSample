# SQLiteSample

Demonstrates usage of SQLite and EF6 in a console app

# Prerequisites

* System.Data.SQLite 1.0.94.1 
* Chinook SQLite Database
* EF6

# Setup

* Create Blank Visual Studio Solution
* Create Class Library Project
* Create Console project
* Install packages:
* `PM> Install-Package ChinookDatabase.Sqlite`
* `PM> Install-Package System.Data.SQLite`
* Installing SQLite also installs EF6 
* `Update-Package or Update-Package –reinstall <package name>`
* Leave `<package name>` blank to reinstall all

# Errors

## Config related errors

`Column 'InvariantName' is constrained to be unique.  Value 'System.Data.SQLite' is already present.`

    <?xml version="1.0" encoding="utf-8"?>
    <configuration>
        <entityFramework>
            <defaultConnectionFactory type="System.Data.Entity.Infrastructure.LocalDbConnectionFactory, EntityFramework">
              <parameters>
                <parameter value="mssqllocaldb" />
              </parameters>
            </defaultConnectionFactory>
            <providers>
                <provider invariantName="System.Data.SqlClient" type="System.Data.Entity.SqlServer.SqlProviderServices, EntityFramework.SqlServer" />
                <provider invariantName="System.Data.SQLite.EF6" type="System.Data.SQLite.EF6.SQLiteProviderServices, System.Data.SQLite.EF6" />
                <provider invariantName="System.Data.SQLite" type="System.Data.SQLite.EF6.SQLiteProviderServices, System.Data.SQLite.EF6" />
            </providers>
        </entityFramework>
        <system.data>
            <!--
                NOTE: The extra "remove" element below is to prevent the design-time
                      support components within EF6 from selecting the legacy ADO.NET
                      provider for SQLite (i.e. the one without any EF6 support).  It
                      appears to only consider the first ADO.NET provider in the list
                      within the resulting "app.config" or "web.config" file.
            -->
            <DbProviderFactories>
                <remove invariant="System.Data.SQLite" />
                <remove invariant="System.Data.SQLite.EF6" />
                <add name="SQLite Data Provider" invariant="System.Data.SQLite" description=".NET Framework Data Provider for SQLite" type="System.Data.SQLite.SQLiteFactory, System.Data.SQLite" />
                <add name="SQLite Data Provider (Entity Framework 6)" invariant="System.Data.SQLite.EF6" description=".NET Framework Data Provider for SQLite (Entity Framework 6)" type="System.Data.SQLite.EF6.SQLiteProviderFactory, System.Data.SQLite.EF6" />
            </DbProviderFactories>
          </system.data>
    </configuration>
    
Not sure why, but make sure to have the config for entityFramework and DbProviderFactories this way.    

## Reference to complex property in entity class requires specifying the property that references the complex property

    SQL logic error or missing database
    no such column: Extent1.Artist_ArtistId

    namespace SQLiteSample.Entities
    {
        public class Album
        {
            public long AlbumId { get; set; }

            public string Title { get; set; }

            public long ArtistId { get; set; } // Required for sqlite to properly reference Artist

            public virtual Artist Artist { get; set; }
        }
    }

## Caveats

EF6 with SQLite still does not support Code First migrations.

Your migration code may need to use the SQLite api directly.

# References

* http://stackoverflow.com/questions/22174212/entity-framework-6-with-sqlite-3-code-first-wont-create-tables/23128288#23128288
* http://hintdesk.com/sqlite-with-entity-framework-code-first-and-migration/
* https://damienbod.wordpress.com/2013/11/18/using-sqlite-with-entity-framework-6-and-the-repository-pattern/
* http://brice-lambson.blogspot.no/2012/10/entity-framework-on-sqlite.html







