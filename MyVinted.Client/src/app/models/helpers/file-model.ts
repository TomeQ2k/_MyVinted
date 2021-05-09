import { Guid } from "guid-typescript";

export class FileModel {
  id: Guid;
  url: string;
  file: File;

  constructor(url: string, file: File) {
    this.id = Guid.create();
    this.url = url;
    this.file = file;
  }
}
