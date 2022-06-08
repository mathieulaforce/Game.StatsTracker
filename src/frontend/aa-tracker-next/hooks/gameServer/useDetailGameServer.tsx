import { useState, useEffect } from 'react';
import axios from 'axios';
import { GameSession } from '../../types/temp';
const useDetailGameServer = (ip: string | null): [boolean, GameSession | undefined] => {
  const [isLoading, setIsLoading] = useState(false);
  const [server, setServer] = useState<GameSession | undefined>();

  useEffect(() => {
    const loadServer = async () => {
      try {
        setIsLoading(true);
        const result = await axios.get(' http://localhost:7071/api/Servers/' + ip);
        return result.data;
      } catch (e) {
        console.error(e);
        return [];
      }
    };

    if (!ip) {
      return;
    }

    loadServer().then((result) => {
      setServer(result);
      setIsLoading(false);
    });
  }, [ip]);

  return [isLoading, server];
};

export default useDetailGameServer;
