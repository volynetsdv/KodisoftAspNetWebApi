using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using KodisoftAspNetWebApi.Models;
using KodisoftAspNetWebApi;

namespace KodisoftAspNetWebApi.Migrations
{
    [DbContext(typeof(FeedsContext))]
    [Migration("20170713200008_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("KodisoftAspNetWebApi.Models.FeedItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description")
                        .IsRequired();

                    b.Property<int>("FeedType");

                    b.Property<int?>("FeedURLsId");

                    b.Property<int>("FeedUrlId");

                    b.Property<string>("Image");

                    b.Property<string>("ResourceUrl");

                    b.Property<string>("Title")
                        .IsRequired();

                    b.Property<DateTime>("UpdatedOn");

                    b.HasKey("Id");

                    b.HasIndex("FeedURLsId");

                    b.ToTable("Items");
                });

            modelBuilder.Entity("KodisoftAspNetWebApi.Models.FeedList", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("FeedUrl")
                        .IsRequired();

                    b.Property<string>("Group");

                    b.Property<string>("SubGroup");

                    b.HasKey("Id");

                    b.ToTable("UrlsList");
                });

            modelBuilder.Entity("KodisoftAspNetWebApi.Models.FeedItem", b =>
                {
                    b.HasOne("KodisoftAspNetWebApi.Models.FeedList", "FeedURLs")
                        .WithMany("FeedItems")
                        .HasForeignKey("FeedURLsId");
                });
        }
    }
}
