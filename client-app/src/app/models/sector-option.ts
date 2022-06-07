export interface SectorOption {
    id: string
    label: string
    level: number
    children: SectorOption[]
}