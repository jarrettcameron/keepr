import { AppState } from "../AppState";
import { Vault } from "../models/Vault";
import { router } from "../router";
import { api } from "./AxiosService";

class VaultsService
{
    async destroyVault(id) {
        await api.delete(`api/vaults/${id}`)
        const mvIndex = AppState.myVaults.findIndex(x => x.id == id)
        if (mvIndex != -1) AppState.myVaults.splice(mvIndex, 1);
        AppState.activeVault = null
        await router.push({ name: 'Home', force: true })
    }
    async createVault(vaultData) {
        const response = await api.post(`api/vaults`, vaultData)
        AppState.activeVaults.unshift(new Vault(response.data))
        AppState.myVaults.unshift(new Vault(response.data))
    }
    async getVaultById(vaultId) {
        AppState.activeVault = null
        AppState.keeps = null
        const response = await api.get(`api/vaults/${vaultId}`)
        AppState.activeVault = new Vault(response.data)
    }
    async getVaultsOfProfile(profileId) {
        const response = await api.get(`api/profiles/${profileId}/vaults`)
        AppState.activeVaults = response.data.map(o => new Vault(o))
    }
}

export const vaultsService = new VaultsService()