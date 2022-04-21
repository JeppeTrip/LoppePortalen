using Application.Booths.Queries.GetUsersBooths;
using Application.Common.Models;
using Application.Markets.Queries.GetMarket;
using Application.Markets.Queries.GetUsersMarkets;
using Application.Merchants.Queries.GetUsersMerchants;
using Application.Organisers.Queries.GetUsersOrganisers;
using Application.Stalls.Queries.GetStall;
using Application.StallTypes.Queries.GetStallType;
using Application.User.Queries.GetUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Web.Controllers
{
    public class UserController : ApiBase
    {
        [HttpGet]
        public async Task<ActionResult<GetUserResponse>> GetUserInfo()
        {
            var userId = CurrentUserService.UserId;
            return await Mediator.Send(new GetUserQuery() { Dto = new GetUserRequest() { UserId = userId } });
        }

        [HttpPut]
        public async Task<ActionResult<GetUserResponse>> UpdateUserInfo()
        {
            var userId = CurrentUserService.UserId;
            return await Mediator.Send(new GetUserQuery() { Dto = new GetUserRequest() { UserId = userId } });
        }

        [HttpGet("markets")]
        public async Task<ActionResult<GetUsersMarketsResponse>> GetUsersMarkets()
        {
            var userid = CurrentUserService.UserId;
            return await Mediator.Send(new GetUsersMarketsQuery() { Dto = new GetUsersMarketsRequest() { UserId = userid } });
        }

        [HttpGet("organisers")]
        public async Task<ActionResult<GetUsersOrganisersResponse>> GetUsersOrganisers()
        {
            var userid = CurrentUserService.UserId;
            return await Mediator.Send(new GetUsersOrganisersQuery() { Dto = new GetUsersOrganisersRequest() { UserId = userid } });
        }

        [HttpGet("merchants")]
        public async Task<ActionResult<GetUsersMerchantsResponse>> GetUsersMerchants()
        {
            return await Mediator.Send(new GetUsersMerchantsQuery());
        }

        [HttpGet("booths")]
        public async Task<ActionResult<List<Booth>>> GetUsersBooths()
        {
            var boothsResponse = await Mediator.Send(new GetUsersBoothsQuery());
            if (boothsResponse == null || boothsResponse.Booths.Count == 0)
            {
                return NotFound();
            }
            //for each booth find the related stalls, their stall type, and their related market.
            List<Booth> boothList = new List<Booth>();
            foreach(var boothResponse in boothsResponse.Booths)
            {
                var stallResponse = await Mediator.Send(new GetStallQuery()
                {
                    Dto = new GetStallRequest() { StallId = boothResponse.StallId }
                });

                var stallTypeResponse = await Mediator.Send(new GetStallTypeQuery() { Dto=new GetStallTypeRequest() { StallTypeId = stallResponse.StallTypeId} });
                var marketResponse = await Mediator.Send(new GetMarketInstanceQuery() { Dto = new GetMarketInstanceQueryRequest() { MarketId = stallResponse.MarketInstanceId } });
                var boothvm = new Booth()
                {
                    Id = boothResponse.Id,
                    BoothName = boothResponse.BoothName,
                    BoothDescription = boothResponse.BoothDescription,
                    Stall = new Stall()
                    {
                        Id = stallResponse.StallId,
                        Market = marketResponse.Market,
                        StallType = new StallType()
                        {
                            Id = stallTypeResponse.StallTypeId,
                            Name = stallTypeResponse.Name,
                            Description = stallTypeResponse.Description,
                            Market = marketResponse.Market,
                            TotalStallCount = 0

                        }
                    }
                };
                boothList.Add(boothvm);
            }

            return boothList;
        }

    }
}
