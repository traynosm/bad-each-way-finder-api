﻿using bad_each_way_finder_api_domain.DomainModel;

namespace bad_each_way_finder_api_domain.CommonInterfaces
{
    public interface IPropositionDatabaseService
    {
        void AddProposition(Proposition proposition);
    }
}
