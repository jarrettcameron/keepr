import { AppState } from "../AppState";
import { Keep } from "../models/Keep";
import { api } from "./AxiosService";

class KeepsService {
    setActive(keep) {
        AppState.activeKeep = keep;
    }
    async getKeeps() {
        const response = await api.get('api/keeps')
        AppState.keeps = response.data.map(o => new Keep(o))
    }
}

export const keepsService = new KeepsService();