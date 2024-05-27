import { AppState } from "../AppState";
import { Keep } from "../models/Keep";
import { api } from "./AxiosService";

class KeepsService {
    async setActive(keep) {
        const response = await api.get(`api/keeps/${keep.id}`)
        AppState.activeKeep = new Keep(response.data)
    }
    async getKeeps() {
        const response = await api.get('api/keeps')
        AppState.keeps = response.data.map(o => new Keep(o))
    }

    async getKeepsOfProfile(profileId) {
        const response = await api.get(`api/profiles/${profileId}/keeps`)
        AppState.keeps = response.data.map(o => new Keep(o))
    }
}

export const keepsService = new KeepsService();