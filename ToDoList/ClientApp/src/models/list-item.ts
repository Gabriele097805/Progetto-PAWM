export type ListItem = {
    readonly id: number
    readonly checked: boolean
    readonly text: string
}

export type List = readonly ListItem[]

export type ListInfo = {
    readonly id: number
    readonly name: string
    readonly items?: List
}

// TODO
export type UserLists = readonly ListInfo[]

export const getEmptyItem = (): ListItem => ({
    id: 0,
    checked: false,
    text: "",
})

export const addNewItem = (state: List): List => [
    ...state,
    {
        id: state.length + 1,
        checked: false,
        text: "",
    },
]

export const update = (state: List, item: ListItem): List =>
    state.map((i) => {
        if (i.id === item.id) return item
        return i
    })
