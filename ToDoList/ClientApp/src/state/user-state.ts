import { atom, selector, useRecoilValue } from "recoil"
import { getUser, type User } from "../models/user"

const userState = atom<User | undefined>({
    key: "userState",
    default: undefined,
    effects: [
        ({ setSelf }) => {
            getUser()
                .then((user) => {
                    setSelf(user)
                })
                .catch((error) => {
                    console.error(
                        "The user information couldn't be fetched",
                        error
                    )

                    setSelf(undefined)
                })
        },
    ],
})

const userSelector = selector<User | undefined>({
    key: "userSelector",
    get: ({ get }) => {
        return get(userState)
    },
})

export const useUser = () => {
    return useRecoilValue(userSelector)
}
