import { render, screen } from "@testing-library/react"
import { ListInfo } from "../../models/list-item.ts"
import * as useListsStateFromParams from "../list/shared.ts"
import Title from "../list/Title.tsx"
import { vi } from "vitest"

describe("Title", () => {
    beforeAll(() => {
        const mockList: ListInfo = {
            id: 1,
            name: "test",
            items: [
                {
                    id: 1,
                    text: "test",
                    checked: true,
                },
            ],
        }

        vi.spyOn(
            useListsStateFromParams,
            "useListsStateFromParams"
        ).mockImplementation(() => ({
            list: mockList,
            setList: (_id, _item) => {},
        }))

        vi.mock("react-router-dom", () => ({
            useNavigate: () => () => {},
        }))
    })

    it("Should render the title component", () => {
        render(<Title />)
        const title = screen.getByText("test")
        expect(title).toBeInTheDocument()
    })
})
