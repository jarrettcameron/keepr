import { Modal } from "bootstrap"
import { AppState } from "../AppState"
import { api } from "./AxiosService"

class VaultKeepsService {
    async createVaultKeep(formData) {
        await api.post('api/vaultkeeps', formData)
        AppState.activeKeep.kept++
    }
    async destroyVaultKeep(keep)
    {
        await api.delete(`api/vaultkeeps/${keep.vaultKeepId}`)
        const indexOf = AppState.keeps.findIndex(x => x.id == keep.id)
        if (indexOf != -1) {
            AppState.keeps.splice(indexOf, 1)
        }
        Modal.getInstance('#keepModal')?.hide()
        AppState.activeKeep = null
    }
}

export const vaultKeepsService = new VaultKeepsService()