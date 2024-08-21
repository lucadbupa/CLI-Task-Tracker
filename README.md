# CLI-Task-Tracker

This is a command-line application designed to help you manage your to-do list efficiently. This tool allows you to add, update, delete, and track the status of your tasks through a simple and intuitive command-line interface. Tasks are stored in a JSON file, making them easy to manage and persist across sessions.

## Commands

### Adding a new task
- `add "Buy groceries"`

  Adds a new task with the description "Buy groceries".

### Updating a task
- `update 1 "Buy groceries and cook dinner"`

  Updates the task with ID 1 to the new description "Buy groceries and cook dinner".

### Deleting a task
- `delete 1`

  Deletes the task with ID 1.

### Marking a task as in progress
- `mark-in-progress 1`

  Marks the task with ID 1 as "in-progress".

### Marking a task as done
- `mark-done 1`

  Marks the task with ID 1 as "done".

### Listing all tasks
- `list`

  Lists all the tasks.

### Listing tasks by status

- `list done`

  Lists all tasks that are marked as "done".

- `list todo`

  Lists all tasks that are yet to be done (status "todo").

- `list in-progress`

  Lists all tasks that are currently "in-progress".
