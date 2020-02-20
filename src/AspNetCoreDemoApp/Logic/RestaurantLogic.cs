using AspNetCoreDemoApp.Model;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AspNetCoreDemoApp.Logic
{
    public class RestaurantLogic
    {
        private readonly AppDbContext _dbContext;
        public RestaurantLogic(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<JapanRestaurant> GetRestaurant(
            string week, string openTime , string closeTime,
            string type, string star,
            bool? parking, bool? uber, bool? deposit,
            string position, int page, int limit)
        {
            var query = _dbContext.JapanRestaurant.AsQueryable();
            QueryAndCondition(ref query, type, star, parking, uber, deposit, position);

            var result = query.ToList();
            if (!string.IsNullOrWhiteSpace(week)
                && !string.IsNullOrWhiteSpace(openTime)
                && !string.IsNullOrWhiteSpace(closeTime))
            {
                switch (week)
                {
                    case "7":
                    case "日":
                        result = result
                            .Where(x => x.W7Time.Contains("-"))
                            .Where(x => Convert.ToDateTime(x.W7Time.Split('-').First()) >= Convert.ToDateTime(openTime))
                            .Where(x => Convert.ToDateTime(x.W7Time.Split('-').Last()) <= Convert.ToDateTime(closeTime))
                            .ToList();
                        break;

                    case "1":
                    case "一":
                        result = result
                            .Where(x => x.W1Time.Contains("-"))
                            .Where(x => Convert.ToDateTime(x.W1Time.Split('-').First()) >= Convert.ToDateTime(openTime))
                            .Where(x => Convert.ToDateTime(x.W1Time.Split('-').Last()) <= Convert.ToDateTime(closeTime))
                            .ToList();
                        break;

                    case "2":
                    case "二":
                        result = result
                            .Where(x => x.W2Time.Contains("-"))
                            .Where(x => Convert.ToDateTime(x.W2Time.Split('-').First()) >= Convert.ToDateTime(openTime))
                            .Where(x => Convert.ToDateTime(x.W2Time.Split('-').Last()) <= Convert.ToDateTime(closeTime))
                            .ToList();
                        break;

                    case "3":
                    case "三":
                        result = result
                            .Where(x => x.W3Time.Contains("-"))
                            .Where(x => Convert.ToDateTime(x.W3Time.Split('-').First()) >= Convert.ToDateTime(openTime))
                            .Where(x => Convert.ToDateTime(x.W3Time.Split('-').Last()) <= Convert.ToDateTime(closeTime))
                            .ToList();
                        break;

                    case "4":
                    case "四":
                        result = result
                            .Where(x => x.W4Time.Contains("-"))
                            .Where(x => Convert.ToDateTime(x.W4Time.Split('-').First()) >= Convert.ToDateTime(openTime))
                            .Where(x => Convert.ToDateTime(x.W4Time.Split('-').Last()) <= Convert.ToDateTime(closeTime))
                            .ToList();
                        break;

                    case "5":
                    case "五":
                        result = result
                            .Where(x => x.W5Time.Contains("-"))
                            .Where(x => Convert.ToDateTime(x.W5Time.Split('-').First()) >= Convert.ToDateTime(openTime))
                            .Where(x => Convert.ToDateTime(x.W5Time.Split('-').Last()) <= Convert.ToDateTime(closeTime))
                            .ToList();
                        break;

                    case "6":
                    case "六":
                        result = result
                            .Where(x => x.W6Time.Contains("-"))
                            .Where(x => Convert.ToDateTime(x.W6Time.Split('-').First()) >= Convert.ToDateTime(openTime))
                            .Where(x => Convert.ToDateTime(x.W6Time.Split('-').Last()) <= Convert.ToDateTime(closeTime))
                            .ToList();
                        break;
                }
            }

            if (result != null)
            {
                result = result.Skip((page - 1) * limit)
                .Take(limit)
                .ToList();
            }

            return result;
        }

        private void QueryAndCondition(ref IQueryable<JapanRestaurant> query,
            string type, string star, bool? parking, bool? uber, bool? deposit, string position)
        {
            if (!string.IsNullOrWhiteSpace(type))
            {
                query = query.Where(x => x.Type == type);
            }
            if (!string.IsNullOrWhiteSpace(star))
            {
                query = query.Where(x => x.Star == star);
            }
            if (parking.HasValue)
            {
                query = query.Where(x => x.Parking == parking.Value);
            }
            if (uber.HasValue)
            {
                query = query.Where(x => x.Uber == uber.Value);
            }
            if (deposit.HasValue)
            {
                query = query.Where(x => x.Deposit == deposit.Value);
            }
            if (!string.IsNullOrWhiteSpace(position))
            {
                query = query.Where(x => x.Position == position);
            }
        }

        public IEnumerable<JapanRestaurant> GetRestaurantOrCondition(
            string week, string openTime, string closeTime,
            string type, string star,
            bool? parking, bool? uber, bool? deposit,
            string position, int page, int limit)
        {
            var queryString = "select * from \"JapanRestaurant\" where  ";
            var whereString = string.Empty;

            if (!string.IsNullOrWhiteSpace(type))
            {
                whereString = whereString + " \"Type\" = '" + type
                    .Replace("'",string.Empty)
                    .Replace(";",string.Empty) + "'";
            }
            if (!string.IsNullOrWhiteSpace(star))
            {
                if (!string.IsNullOrEmpty(whereString))
                {
                    whereString += " or ";
                }
                whereString = whereString + " or \"Star\" = '" + star
                .Replace("'", string.Empty)
                .Replace(";", string.Empty) + "'";
            }
            if (parking.HasValue)
            {
                if (!string.IsNullOrEmpty(whereString))
                {
                    whereString += " or ";
                }
                whereString = whereString + " or \"Parking\" = " + parking.Value.ToString().ToLower();
            }
            if (uber.HasValue)
            {
                if (!string.IsNullOrEmpty(whereString))
                {
                    whereString += " or ";
                }
                whereString = whereString + " or \"Uber\" = " + uber.Value.ToString().ToLower();
            }
            if (deposit.HasValue)
            {
                if (!string.IsNullOrEmpty(whereString))
                {
                    whereString += " or ";
                }
                whereString = whereString + " or \"Deposit\" = " + deposit.Value.ToString().ToLower();
            }
            if (!string.IsNullOrWhiteSpace(position))
            {
                if (!string.IsNullOrEmpty(whereString))
                {
                    whereString += " or ";
                }
                whereString = whereString + " or \"Position\" = '" + position
                    .Replace("'", string.Empty)
                    .Replace(";", string.Empty) + "'";
            }

            queryString = whereString + " limit " + limit + " offset " + (page - 1) * limit;


            return _dbContext.JapanRestaurant.FromSqlRaw(queryString).ToList();
        }
    }
}
