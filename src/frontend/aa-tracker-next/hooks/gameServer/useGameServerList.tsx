import { useState, useEffect } from 'react';
import axios from 'axios';
import { GameServer } from '../../types/gameServer';
const useGameServerList = (): [boolean, GameServer[]] => {
  const [isLoading, setIsLoading] = useState(false);
  const [servers, setServers] = useState<GameServer[]>([]);

  useEffect(() => {
    const loadServers = async () => {
      try {
        setIsLoading(true);
        const result = await axios.get(' http://localhost:7071/api/Servers');
        return result.data;
      } catch (e) {
        console.error(e);
        return [];
      }
    };
    loadServers().then((result) => {
      setServers(result);
      setIsLoading(false);
    });
  }, []);

  return [isLoading, servers];
};

export default useGameServerList;
