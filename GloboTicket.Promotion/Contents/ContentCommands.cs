﻿using GloboTicket.Promotion.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace GloboTicket.Promotion.Contents
{
    public class ContentCommands
    {
        private readonly PromotionContext repository;

        public ContentCommands(PromotionContext repository)
        {
            this.repository = repository;
        }

        public async Task<string> SaveContent(byte[] binary, string contentType)
        {
            var sha512 = HashAlgorithm.Create(HashAlgorithmName.SHA512.Name);
            var hash = Convert.ToBase64String(sha512.ComputeHash(binary));
            // avoid any slashes, plus signs or equal signs
            // the following makes this base64 string url safe
            hash = hash.Replace("/", "_");
            hash = hash.Replace("+", "-");

            var exists = await repository.Content
                .Where(c => c.Hash == hash)
                .AnyAsync();

            if (!exists)
            {
                await repository.Content.AddAsync(new Content
                {
                    Hash = hash,
                    Binary = binary,
                    ContentType = contentType
                });
                await repository.SaveChangesAsync();
            }

            return hash;
        }
    }
}
