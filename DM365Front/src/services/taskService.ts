export async function getTasks() {
    try {
      const response = await fetch('https://localhost:7253/api/Task');
      if (!response.ok) throw new Error('Ошибка получения данных');
      return await response.json();
    } catch (err) {
      console.error(err);
      return [];
    }
  }
  