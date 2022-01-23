﻿// <auto-generated />
using System;
using EfBloggyImproved;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EfBloggyImproved.Migrations
{
    [DbContext(typeof(BlogContext))]
    [Migration("20220123102210_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.13")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("EfBloggyImproved.BlogAuthor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Author")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("BlogPostId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateAdded")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("BlogAuthors");
                });

            modelBuilder.Entity("EfBloggyImproved.BlogPost", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AuthorId")
                        .HasColumnType("int");

                    b.Property<DateTime>("PostTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("blogAuthorId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("blogAuthorId");

                    b.ToTable("BlogPosts");
                });

            modelBuilder.Entity("EfBloggyImproved.BlogPost", b =>
                {
                    b.HasOne("EfBloggyImproved.BlogAuthor", "blogAuthor")
                        .WithMany("blogPost")
                        .HasForeignKey("blogAuthorId");

                    b.Navigation("blogAuthor");
                });

            modelBuilder.Entity("EfBloggyImproved.BlogAuthor", b =>
                {
                    b.Navigation("blogPost");
                });
#pragma warning restore 612, 618
        }
    }
}
