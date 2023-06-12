import { useState } from "react"
import { atom, selector, useSetRecoilState } from "recoil"

const tooltipAutomaticDisappearTimeMs = 5_000

export type AppMessageIntent = "success" | "warning" | "error" | "info"

export type PageError = {
    show: boolean
    intent?: AppMessageIntent
    message?: string
}

const appMessageAtom = atom<PageError>({
    key: "error",
    default: {
        show: false,
        intent: "error",
    },
    effects: [],
})

export const appMessageSelector = selector({
    key: "appMessageSelector",
    get: ({ get }) => get(appMessageAtom),
})

export const useTooltipMessage = () => {
    const setError = useSetRecoilState(appMessageAtom)
    const [timerId, setTimerId] = useState<NodeJS.Timeout | undefined>()

    const setTooltipMessage = (message: string, intent?: AppMessageIntent) => {
        setError({ show: true, intent: intent ?? "info", message })

        const newTimerId = setTimeout(() => {
            setError({ show: false })
        }, tooltipAutomaticDisappearTimeMs)

        setTimerId(newTimerId)
    }

    const dismissTooltipMessage = () => {
        if (timerId != null) {
            clearTimeout(timerId)
        }

        setError({ show: false })
    }

    return { setTooltipMessage, dismissTooltipMessage }
}
