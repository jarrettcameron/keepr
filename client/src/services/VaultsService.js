import { AppState } from "../AppState";
import { Vault } from "../models/Vault";
import { api } from "./AxiosService";

class VaultsService
{
    async getVaultsOfProfile(profileId) {
        const response = await api.get(`api/profiles/${profileId}/vaults`)
        AppState.activeVaults = response.data.map(o => new Vault(o))
    }
}

export const vaultsService = new VaultsService()