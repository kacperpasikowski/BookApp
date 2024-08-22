export interface Message {
    id: string
    senderId: string
    senderUsername: string
    recipientId: string
    recipientUsername: string
    content: string
    dateRead?: string
    messageSent: string
  }
  