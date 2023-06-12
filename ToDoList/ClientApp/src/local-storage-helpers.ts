/**
 * Gets a parsed item from local storage, or returns a default value if it doesn't exist
 * @param key The key of the item to get.
 * @param defaultValue The default value to return if the item doesn't exist.
 * @returns The parsed item from local storage, or the default value if it doesn't exist.
 */
export const getLocalStorageItem = <T>(key: string, defaultValue?: T) => {
    const item = localStorage.getItem(key)

    if (item) {
        const parsed = JSON.parse(item) as T

        if (parsed != null) {
            return parsed
        }
    }

    return defaultValue
}

/**
 * Sets an item in local storage. The item can be an object, it will be serialized automatically to JSON.
 * @param key The key of the item to set.
 * @param value The value of the item to set.
 */
export const setLocalStorageItem = <T>(key: string, value: T) =>
    typeof value === "string"
        ? localStorage.setItem(key, value)
        : localStorage.setItem(key, JSON.stringify(value))

export const clearLocalStorage = (key: string) => localStorage.removeItem(key)
