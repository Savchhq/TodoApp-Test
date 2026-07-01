export interface TaskDto {
  id: string;
  title: string;
  description?: string;
  isCompleted: boolean;
  categoryId: string;
  categoryName?: string;
}

export interface GetTasksQuery {
  searchQuery?: string;
  categoryId?: string;
  pageNumber?: number;
  pageSize?: number;
}

export interface TasksPagedResult {
  items: TaskDto[];
  totalCount: number;
}

export interface UpdateTaskDto {
  title: string;
  description?: string;
  isCompleted: boolean;
  categoryId: string;
}