export interface LogModel {
  date: Date;
  message: string;
  level: string;
  exception: string;
  requestMethod: string;
  requestPath: string;
  statusCode?: number;
  elapsed?: number;
  sourceContext: string;
  requestId: string;
  connectionId: string;
}
