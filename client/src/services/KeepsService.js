import { Modal } from "bootstrap";
import { AppState } from "../AppState";
import { Keep } from "../models/Keep";
import { api } from "./AxiosService";

class KeepsService {
    async createKeep(keepData) {
        const response = await api.post(`api/keeps`, keepData)
        AppState.keeps.unshift(new Keep(response.data))
    }
    async destroyKeep(id) {
        await api.delete(`api/keeps/${id}`)
        const index = AppState.keeps.findIndex(x => x.id == id)
        if (index != -1) {
            AppState.keeps.splice(index, 1)
        }
        Modal.getInstance('#keepModal')?.hide()
        AppState.activeKeep = null
    }
    async getKeepsFromVault(vaultId) {
        AppState.keeps = null
        const response = await api.get(`api/vaults/${vaultId}/keeps`)
        AppState.keeps = response.data.map(o => new Keep(o))
    }
    async setActive(keep) {
        const response = await api.get(`api/keeps/${keep.id}`)
        AppState.activeKeep = new Keep(response.data)
        if (keep.vaultKeepId != '') AppState.activeKeep.vaultKeepId = keep.vaultKeepId
    }
    async getKeeps() {
        AppState.keeps = null
        const response = await api.get('api/keeps')
        AppState.keeps = response.data.map(o => new Keep(o))
    }

    async getKeepsOfProfile(profileId) {
        const response = await api.get(`api/profiles/${profileId}/keeps`)
        AppState.keeps = response.data.map(o => new Keep(o))
    }
}

export const keepsService = new KeepsService();