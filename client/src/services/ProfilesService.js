import { AppState } from '../AppState'
import { Profile } from '../models/Profile'
import { api } from './AxiosService'

class ProfilesService {
    async getProfile(profileId) {
        const response = await api.get(`api/profiles/${profileId}`)
        AppState.activeAccount = new Profile(response.data)
    }
}

export const profilesService = new ProfilesService()